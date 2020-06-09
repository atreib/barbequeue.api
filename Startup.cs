using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using barbequeue.api.Infra.Db.SQLServer.Contexts;
using barbequeue.api.Data.Protocols;
using barbequeue.api.Infra.Db.SQLServer.Repositories;
using barbequeue.api.Domain.UseCases;
using barbequeue.api.Data.UseCases;
using barbequeue.api.Helpers.Jwt;
using barbequeue.api.Helpers.Encrypter;
using barbequeue.api.Helpers;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace barbequeue.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // reading our app settings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configurating our middleware
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configurating swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "BarbeQUEUE API", 
                    Version = "v1",
                    Contact = new OpenApiContact
                        {
                            Name = "André Treib",
                            Email = "andre.schumacher.treib@gmail.com"
                        },
                });
                c.AddSecurityDefinition(
                    "bearer",
                    new OpenApiSecurityScheme {
                        In = ParameterLocation.Header,
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    }
                );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DevDatabase"));
            });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IBarbequeRepository, BarbequeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBarbequeParticipantRepository, BarbequeParticipantRepository>();
            services.AddScoped<IBarbequeService, BarbequeService>();
            services.AddScoped<IBarbequeParticipantService, BarbequeParticipantService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // uncomment this to recreate database
            /*using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarbeQUEUE API v1");
            });
        }
    }
}
