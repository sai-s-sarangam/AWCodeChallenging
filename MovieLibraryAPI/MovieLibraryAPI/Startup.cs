﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppServices.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieLibraryAPI.Convention;
using MovieLibraryAPI.Middleware;
using MoviesLibraryAPI.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace MovieLibraryAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(c => c.Conventions.Add(new ApiExplorerGroupPerVersionConvention())).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var ApiDescription = Configuration["Description"];

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "MovieLibrary.API",
                        Version = "v1",
                        Description = $"{ApiDescription}. " +
                        $"Api Version :1", 
                        License = new License
                        {
                            Name = "Coding Challenge"
                        },
                        Contact = new Contact
                        {
                            Name = "Sai Sarangam",
                            Email = "sai.sarangam09@gmail.com"
                        }
                    });     

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
                //Locate the XML file being generated by ASP.NET...
                xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name.Replace("MovieLibraryAPI", "Entities")}.XML";
                xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);

                // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/244

                c.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", apiDescription.GroupName));

                    apiDescription.RelativePath = string.Join("/", values);

                    var versionParameter = apiDescription.ParameterDescriptions.SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                    {
                        apiDescription.ParameterDescriptions.Remove(versionParameter);
                    }

                    return apiDescription.GroupName == version;
                });
            });

            //SCRControl
            services.AddTransient<IMoviesDbConfig>(s => new MoviesDbConfig());
            services.AddTransient<IMoviesManager, MoviesManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string appName = "";

            app.UseCors("CorsPolicy");

            //enable middleware to serve generated Swgger as a JSON endpoint.
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                appName = "MovieLibraryAPI";
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandler>();

            //Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // speicifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{appName}/swagger/v1/swagger.json", "MovieLibraryAPI v1"); 
                //to serve the swagger UI at the app's root (http://localhost:<port>/), set the RoutePrefix property to an empty string:
                c.RoutePrefix = string.Empty;
            });
            //app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
