using MatrixEC.Models.Context;
using MatrixEC.Repository.CategoryReprository;
using MatrixEC.Repository.ProductReprository;
using MatrixEC.Repository.PropertyReprositry;
using Microsoft.EntityFrameworkCore;

namespace MatrixEC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MatrixContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
            });

            builder.Services.AddScoped<ICategoryReprositry, CategotyReprository>();
            builder.Services.AddScoped<IPropertyReprositry, PropertyReprositry>();
            builder.Services.AddScoped<IProductReprositry, ProductReprositry>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}