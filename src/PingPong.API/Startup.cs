using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PingPong.API.Database;
using PingPong.API.Services;
using System.Threading.Tasks;

namespace PingPong.API
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
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["Database"]));
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IGamesService, GamesService>();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddControllers();
            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseOpenApi();    // default route: /swagger/v1/swagger.json
            app.UseSwaggerUi3(); // default route: /swagger
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", del =>
                {
                    del.Response.Redirect("/swagger/index.html");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}