using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Repositories;
using NikeFarms.v2._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0
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
            services.AddControllersWithViews();
            services.AddDbContext<NikeDbContext>(option => option.UseMySQL(Configuration.GetConnectionString("NikeConnectionString2")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
            {
                config.LoginPath = "/Nike/Login";
                config.LogoutPath = "/Nike/Logout";
                config.Cookie.Name = "NikeAuth2.0";
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IMortalityRepository, MortalityRepository>();

            services.AddScoped<IMortalityService, MortalityService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IDailyActivityRepository, DailyActivityRepository>();

            services.AddScoped<IDailyActivityService, DailyActivityService>();

            services.AddScoped<IExpensesRepository, ExpensesRepository>();

            services.AddScoped<IExpensesService, ExpensesService>();

            services.AddScoped<IFlockRepository, FlockRepository>();

            services.AddScoped<IFlockService, FlockService>();

            services.AddScoped<IFlockTypeRepository, FlockTypeRepository>();

            services.AddScoped<IFlockTypeService, FlockTypeService>();

            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<ISalesItemRepository, SalesItemRepository>();

            services.AddScoped<ISalesItemService, SalesItemService>();

            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddScoped<ISalesService, SalesService>();

            services.AddScoped<IStockRepository, StockRepository>();

            services.AddScoped<IStockService, StockService>();

            services.AddScoped<IStoreAllocationRepository, StoreAllocationRepository>();

            services.AddScoped<IStoreAllocationService, StoreAllocationService>();

            services.AddScoped<IStoreItemRepository, StoreItemRepository>();

            services.AddScoped<IStoreItemService, StoreItemService>();

            services.AddScoped<IWeeklyReportRepository, WeeklyReportRepository>();

            services.AddScoped<IWeeklyReportService, WeeklyReportService>();

            services.AddScoped<IAdminDashboardService, AdminDashboardService>();

        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
