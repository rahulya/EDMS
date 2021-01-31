using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMSAPI.BusinessLogic;
using DMSAPI.BusinessLogic.Company;
using DMSAPI.BusinessLogic.Customer;
using DMSAPI.BusinessLogic.User;
using DMSAPI.Models;
using DMSAPI.Services;
using DMSAPI.SqlGroupRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DMSAPI
{
    public class Startup
    {

        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            var builder = new ConfigurationBuilder();

            var dic = new Dictionary<string, string>
        {
            {"Profile:DBServerName", "DESKTOP-HH86UPU\\RAHUL"},
            {"Profile:DBServerUser", "sa"},
            {"Profile:DBServerPassword", "#$W@$/K753#*D@/@!B@$E#"}
        };
            builder.AddInMemoryCollection(dic);
            Configuration = builder.Build();


        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc()
            //        .AddControllersAsServices();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContextPool<DMSDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("SystemDb")));
           // services.AddScoped(typeof(IGroupRepository<>), typeof(SqlGroupRepository<>));
            services.AddScoped(typeof(IUserRepository<>), typeof(SqlUserRepository<>));
            services.AddScoped(typeof(ICompanyRepository<>), typeof(SqlCompanyRepository<>));
            services.AddScoped(typeof(ICustomerRepository<>), typeof(SqlCustomerRepository<>));
            services.AddScoped(typeof(ConnectionString));
            services.AddScoped(typeof(UserBL));
            services.AddScoped(typeof(UserDL));
            services.AddScoped(typeof(CompanyBL));
            services.AddScoped(typeof(CompanyDL));
            services.AddScoped(typeof(CustomerBL));
            services.AddScoped(typeof(CustomerDL));

            services.AddOptions();
           services.Configure<DbConnectionClass>(Configuration.GetSection("Profile"));


            //string DbName, DbServerName, DbServerUser, DbServerPassword;
            //DbServerName = _configuration.GetSection("Credential:DBServerName").Value;
            //DbServerUser = _configuration.GetSection("Credential:DBServerUser").Value;
            //DbServerPassword = _configuration.GetSection("Credential:DBServerPassword").Value;
            services.AddHttpContextAccessor();


            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:44347", "https://localhost:44347", "https://localhost:4200",
                                        "http://localhost:4200", "http://localhost:4500", "https://localhost:4500").AllowAnyMethod().AllowAnyHeader();
                    builder.WithHeaders("Authorization");
                    builder.WithHeaders("content-type");
                });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //  .AddJwtBearer(options =>
            //  {
            //        // base-address of your identityserver
            //        options.Authority = "http://localhost:5000";

            //        // name of the API resource
            //        options.Audience = "ims_api";
            //  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);

            app.UseSession();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{api}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthentication();


        }
    }
}
