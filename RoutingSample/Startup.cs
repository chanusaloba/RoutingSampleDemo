using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            // Approach 1: Writing a terminal middleware.
            app.Use(next => async context =>
            {
                if (context.Request.Path == "/")
                {
                    await context.Response.WriteAsync("Hello terminal middleware!");
                    return;
                }

                await next(context);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Approach 2: Using routing.
                endpoints.MapGet("/Movie", async context =>
                {
                    await context.Response.WriteAsync("Hello routing!");
                });
            });


            //// Location 1: Before routing runs. Can influence request before routing runs.
            //app.UseHttpMethodOverride();

            //app.UseRouting();

            //// Location 2: After routing runs. Middleware can match based on metadata.
            //app.Use(next => context =>
            //{
            //    var endpoint = context.GetEndpoint();
            //    if (endpoint?.Metadata.GetMetadata<AuditPolicyAttribute>()?.NeedsAudit
            //                                                                    == true)
            //    {
            //        Console.WriteLine($"ACCESS TO SENSITIVE DATA AT: {DateTime.UtcNow}");
            //    }

            //    return next(context);
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello world!");
            //    });

            //    // Using metadata to configure the audit policy.
            //    endpoints.MapGet("/sensitive", async context =>
            //    {
            //        await context.Response.WriteAsync("sensitive data");
            //    })
            //    .WithMetadata(new AuditPolicyAttribute(needsAudit: true));
            //});

            //app.UseAuthorization();

            //// Location 1: before routing runs, endpoint is always null here
            //app.Use(next => context =>
            //{
            //    Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
            //    return next(context);
            //});

            //app.UseRouting();

            //// Location 2: after routing runs, endpoint will be non-null if routing found a match
            //app.Use(next => context =>
            //{
            //    Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
            //    return next(context);
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    // Location 3: runs when this endpoint matches
            //    endpoints.MapGet("/", context =>
            //    {
            //        Console.WriteLine(
            //            $"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
            //        return Task.CompletedTask;
            //    }).WithDisplayName("Hello");
            //});

            //// Location 4: runs after UseEndpoints - will only run if there was no match
            //app.Use(next => context =>
            //{
            //    Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
            //    return next(context);
            //});           

            //app.Use(next => context =>
            //{
            //    var endpoint = context.GetEndpoint();
            //    if (endpoint is null)
            //    {
            //        return Task.CompletedTask;
            //    }

            //    Console.WriteLine($"Endpoint: {endpoint.DisplayName}");

            //    if (endpoint is RouteEndpoint routeEndpoint)
            //    {
            //        Console.WriteLine("Endpoint has route pattern: " +
            //            routeEndpoint.RoutePattern.RawText);
            //    }

            //    foreach (var metadata in endpoint.Metadata)
            //    {
            //        Console.WriteLine($"Endpoint has metadata: {metadata}");
            //    }

            //    return Task.CompletedTask;
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/hello/{name:alpha}", async context =>
            //    {
            //        var name = context.Request.RouteValues["name"];
            //        await context.Response.WriteAsync($"Hello {name}!");
            //    });
            //});


        }
    }

    public class AuditPolicyAttribute : Attribute
    {
        public AuditPolicyAttribute(bool needsAudit)
        {
            NeedsAudit = needsAudit;
        }

        public bool NeedsAudit { get; }
    }
}
