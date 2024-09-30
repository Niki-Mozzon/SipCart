using Microsoft.EntityFrameworkCore;
using SipCartCore.Services;
using SipCartCore.Services.Interfaces;

namespace SipCartApi
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SipCartCore.AppContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("SipCartFE", policy =>
                    policy
                    .WithOrigins("http://localhost:4200")
                    //.AllowAnyOrigin()
                    .AllowAnyHeader().AllowAnyMethod());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("SipCartFE");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}