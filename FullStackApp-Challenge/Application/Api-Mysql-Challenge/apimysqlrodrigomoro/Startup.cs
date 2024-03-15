using apimysqlrodrigomoro.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace apimysqlrodrigomoro
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
            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "3306";
            var password = Configuration["MYSQL_PASSWORD"] ?? Configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = Configuration["MYSQL_USER"] ?? Configuration.GetConnectionString("MYSQL_USER");
            var productsdb = Configuration["MYSQL_DATABASE"] ?? Configuration.GetConnectionString("MYSQL_DATABASE");

            string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";

            services.AddDbContextPool<ApplicationDbContext>(options =>
              options.UseMySql(mySqlConnStr,
                  ServerVersion.AutoDetect(mySqlConnStr)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "apimysqlrodrigomoro", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "apimysqlrodrigomoro v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
