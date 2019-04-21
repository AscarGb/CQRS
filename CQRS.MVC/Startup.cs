using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CQRS.Data;
using CQRS.Domain.Commands;
using CQRS.Domain.Queries;
using Microsoft.AspNetCore.Diagnostics;
using CQRS.Domain.Exceptions;
using CQRS.Domain.Commands.Command;
using CQRS.Common;
using CQRS.Domain.Pipelines;
using CQRS.Domain.Handlers;

namespace CQRS.MVC
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
            services.AddTransient<DbInitializer>();
            services.AddTransient<UserCommandHandlerFactory>();
            services.AddTransient<UserQueryHandlerFactory>();

            //handlers
            services.AddTransient<CheckSecurityUserHandler>();
            services.AddTransient<ValidateUserHandler>();
            //pipes
            services.AddTransient<Pipeline<SaveUserCommand>>();
            //pipes builders
            services.AddSingleton<CreateUserPipelineBuilder>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<IdentityDbContext<IdentityUser>>(options => options.UseSqlite("Data Source=mydb.db"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext<IdentityUser>>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            */

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    var error = feature?.Error;

                    switch (error)
                    {
                        case NotFoundException exc:
                            {
                                context.Response.StatusCode = StatusCodes.Status404NotFound;
                                await context.Response.WriteAsync(exc.Message);
                            }
                            break;
                        case UserCreateException exc:
                            {
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                await context.Response.WriteAsync(exc.Message);
                            }
                            break;
                        case ValidateException exc:
                            {
                                context.Response.StatusCode = StatusCodes.Status418ImATeapot;
                                await context.Response.WriteAsync(exc.Message);
                            }
                            break;
                        default:
                            {
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                await context.Response.WriteAsync("Error");
                            }
                            break;
                    }
                });
            });

            //app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
