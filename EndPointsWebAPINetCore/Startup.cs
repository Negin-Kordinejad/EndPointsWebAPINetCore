using ClientWebAPI.Contracts;
using ClientWebAPI.Services;
using LocalDataProvider.DataServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ClientWebAPI.Models;
using EndPointsWebAPINetCore.Dtos;

namespace EndPointsWebAPINetCore
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
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Journy, JournyDto>();
                //   cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });
            services.AddScoped<IIpProcessor, IpProcessor>()
                    .AddScoped<IJournyEndPoint, JournyEndPoint>()
                    .AddSingleton<IAPIHelper, APIHelper>();

            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer
              (Configuration.GetConnectionString("AppDbContext")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
           {
               options.Password.RequiredLength = 4;
               options.Password.RequiredUniqueChars = 1;
               options.Password.RequireNonAlphanumeric = false;
           }).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtBrearerOptions =>
            {

                jwtBrearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mysequritykeyissecretdonotell")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)

                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EndPointsWebAPI",
                    Version = "v1",
                    Description = "A Coding Challenge ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Negin Kordinejad",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/negin-kordinejad-995a5541/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Jayride",
                        Url = new Uri("https://www.jayride.com/"),
                    }
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "EndPointsWebAPI",
                    Version = "v2",
                    Description = "A Coding Challenge ASP.NET Core Web API version 2",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Negin Kordinejad",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/negin-kordinejad-995a5541/"),
                    },
                   
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n<b>Example: Bearer 1safsfsdfdfb<b> ",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                        }
                    });
            });
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version")
                    );
            }
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions options = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 10
                };

                //  app.UseExceptionHandler("/error-local-development");
                app.UseDeveloperExceptionPage(options);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EndPointsWebAPINetCore v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "EndPointsWebAPINetCore v2");
                });
            }
            else if(env.IsStaging() || env.IsEnvironment("UAT")|| env.IsProduction())
            {
                app.UseExceptionHandler("/error"); //unhandled Errors
             //  app.UseStatusCodePagesWithReExecute("/Error/{0}"); for Mvc for errorcode 
            }
            app.UseHttpsRedirection();
            app.UseFileServer();
       //     app.UseStaticFiles();  app.UseDefaultFiles
     
            app.UseRouting();

            app.UseAuthentication();
            //app.Use(async (Context, next) =>
            //{
            //    if (Context.Request.Headers.Keys.Contains("Authorized"))
            //    {
            //        logger.LogInformation(Context.Request.Body.Length.ToString());
            //        await next();
            //        logger.LogInformation(Context.Response.Body.Length.ToString());
            //    }
            //});
            //app.Use(async (Context, next) =>
            //{
            //    if (Context.Request.Headers.Keys.Contains("Authorized"))
            //    {
            //         await next();
            //    }
            //});
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
