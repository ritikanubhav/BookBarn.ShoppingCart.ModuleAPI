
using System.Text;
using BookBarn.ShoppingCartModule.Data;
using BookBarn.ShoppingCartModule.Domain.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookBarn.ShoppingCart.ModuleAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("default");
            builder.Services.AddDbContext<CartDbContext>(option=>option.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding Jwt Bearer for Verifying Token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "yourIssuer",
                    ValidAudience = "yourAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                };
            });

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins("https://your-allowed-origin.com")
                                     .AllowAnyMethod()
                                     .AllowAnyHeader();
                    });
            });

            //add depencies to inject
            builder.Services.AddTransient<CartDbContext>();
            builder.Services.AddTransient<ICartRepo,CartRepo>();
            builder.Services.AddTransient<ICartItemRepo,CartItemRepo>();


            var app = builder.Build();

            // Configure Migration for Database programatically
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CartDbContext>();
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                // Log or handle migration errors
                throw new Exception($"An error occurred while migrating the database: {ex.Message}", ex);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
