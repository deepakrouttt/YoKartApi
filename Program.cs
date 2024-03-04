using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Services;

namespace YoKartApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<YoKartApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
            // Add services to the container.
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{

            //    options.Cookie.Name = "user_token";
            //    options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
            //    options.LoginPath = "/api/UserApi/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //});

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            //});


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

       
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
