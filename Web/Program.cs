using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace ChasPaint.Web
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ChasPaintDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionString"]));


            builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ChasPaintDbContext>();

            var frontendOrigin = builder.Configuration["frontendOrigin"] ?? throw new Exception("No frontendOrigin found");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Frontend", configurePolicy: p =>
                {
                    p.WithOrigins(frontendOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddAuthorization();

            //SERVICES HJERE
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPointService, PointService>();
            builder.Services.AddScoped<IPointRepository, PointRepository>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddScoped<RegisterUserHandler>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseExceptionHandler();
            app.MapIdentityApi<ApplicationUser>();

            app.UseCors("Frontend");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}