using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webjet.Movies.BusinessLayer.BusinessLogic;
using Webjet.Movies.BusinessLayer.Interfaces;
using Webjet.Movies.BusinessLayer.MappingProfile;
using WebjetMovies.PersistenceLayer.Interfaces;
using WebjetMovies.PersistenceLayer.Model;
using WebjetMovies.PersistenceLayer.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace Webjet.MoviesDBAPI
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

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "Movies API", Version = "v1" });
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies API", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new WebjetMappingProfile());
                x.AddProfile(new MappingProfile());
            });

            services.AddMemoryCache();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddSingleton<CinemaWorldService>();
            services.AddSingleton<FilmWorldService>();
            services.AddSingleton<IMovieService, MovieService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen();

            //services.AddMvcCore();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          
            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();



        }
    }
}
