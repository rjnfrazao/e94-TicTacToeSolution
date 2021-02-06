using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TicTacToeAPI.Data;

namespace TicTacToeAPI
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

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TicTacToeAPI",
                    Version = "v1",
                    Description = "API implementing TicTacToe move performed by a computer.",
                    TermsOfService = new Uri("http://www.fluminense.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ricardo Frazao",
                        Email = "dontcare@nowhere.co"
                    },

               });

                // ** Added according instructions during the classe. Location where xml is saved.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);



                // ** Added based on instructions in class.
                c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
            });

            // CHANGED
            services.AddScoped<ITicTacToeRepo, CodeTicTacToeRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            // ** Added to use the older 2.0 format so add rest client will work.
            app.UseSwagger(c => { c.SerializeAsV2 = true; });

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToeAPI v1");

                // Serve the Swagger UI at the app's root 
                // (http://localhost:<port>)
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
