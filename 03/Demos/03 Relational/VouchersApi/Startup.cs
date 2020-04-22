using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Vouchers {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // SQL Server ... use "SQLServerDBConnection" ConString
            var conStr = Configuration["ConnectionStrings:SQLServerDBConnection"];
            services.AddDbContext<VouchersDBContext> (options =>
                options.UseSqlServer (conStr));

            // SQLite ... use "SQLServerDBConnection" ConString
            // var conStrLite = Configuration["ConnectionStrings:SQLiteDBConnection"];
            // services.AddDbContext<VouchersDBContext> (options =>
            //     options.UseSqlite (conStrLite));

            //Firebase

            var fbProjectId = Configuration["Firebase:ProjectId"];
            services
                .AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.Authority = "https://securetoken.google.com/" + fbProjectId;
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/" + fbProjectId,
                        ValidateAudience = true,
                        ValidAudience = fbProjectId,
                        ValidateLifetime = true
                    };
                });

            //CORS
            var corsUrl = Configuration["FrontendUrl"];
            services.AddCors (options => {
                options.AddPolicy ("CustomCors",
                    builder => builder.WithOrigins (corsUrl)
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
            });

            //Swagger
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Vouchers API", Version = "v1" });
            });

            services.AddControllers ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseStatusCodePages ();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Vouchers API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseCors ("FrontendUrl");

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}