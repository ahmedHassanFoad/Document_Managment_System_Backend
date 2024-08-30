using DMS.Core.Entities.Identity;
using DMS.Core.Services;
using DMS.Repository.Data;
using DMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using DMS.APIs.Profiles;
using DMS.APIs.Filters;
using DMS.Core;
using DMS.Repository;
using DMS.Core.Repository;
using DMS.Repository.Repository;
using Stripe;
using TokenService = DMS.Services.TokenService;
using DMS.APIs.Helpers;
using Microsoft.Extensions.FileProviders;
//using DMS.APIs.Profiles;
namespace DMS.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                    Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot")));
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true; // Optional: for pretty printing JSON
            }); 
            builder.Services.AddDbContext<DMSDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DMS.Repository"));
            });
            


            builder.Services.AddScoped<DirectoryAuthrizationFilter>();
            builder.Services.AddAutoMapper(typeof(RegisterProfile));
           builder.Services.AddAutoMapper(typeof(DirectoryProfile));
            builder.Services.AddAutoMapper(typeof(UserProfile));
            builder.Services.AddAutoMapper(typeof(UserDetailsProfile));



            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDirectoryRepository, DirectoryRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IWorkSpaceRepository, WorkSpaceReposiotry>();
            builder.Services.AddScoped<IWorkSpaceService, WorkSpaceService>();
            builder.Services.AddScoped<IDirectoryService, DirectoryService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
           

            builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<DMSDbContext>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["jwt:ValidIssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["jwt:ValidAudiance"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))
                };
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer ", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                Options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer "
                       }
                      },
                      new string[] { }
                    }
                 });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();




            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    SeedData.SeedRoles(roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }


            app.Run();
        }
    }
}
