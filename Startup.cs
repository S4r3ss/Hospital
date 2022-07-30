using Hospital.Context;
using Hospital.Controllers;
using Hospital.Interfaces;
using Hospital.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
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
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HospitalDb;Integrated Security=True"));
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPillsRepository, PillRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<AuthenticationController>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddHttpClient();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Doctor/Error", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Doctor}/{action=Index}/{id?}");
            });
        }
    }
}
