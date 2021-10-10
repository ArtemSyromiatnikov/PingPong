using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PingPong.API.Database;
using PingPong.API.Filters;
using PingPong.API.Services;
using PingPong.API.Validators;
using PingPong.Sdk.Models;

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

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCors(options =>
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = InvalidModelStateHandler;
                    options.SuppressMapClientErrors = true;
                })
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.DictionaryKeyPolicy         = JsonNamingPolicy.CamelCase;
                    jsonOptions.JsonSerializerOptions.IgnoreNullValues            = true;
                    jsonOptions.JsonSerializerOptions.IgnoreReadOnlyProperties    = true;
                    jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy        = JsonNamingPolicy.CamelCase;
                    jsonOptions.JsonSerializerOptions.WriteIndented               = false;
                })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(CreateGameRequestValidator)));
            
            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            
            app.UseRouting();

            app.UseOpenApi();    // default route: /swagger/v1/swagger.json
            app.UseSwaggerUi3(); // default route: /swagger
            app.UseCors();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", del =>
                {
                    del.Response.Redirect("/swagger/index.html");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }

        
        #region Helpers

        private IActionResult InvalidModelStateHandler(ActionContext context)
        {
            var error = new ErrorDto(400, "Validation errors found.", "VALIDATION_FAILED")
            {
                ValidationErrors = ModelStateToDictionary(context.ModelState)
            };
            return new BadRequestObjectResult(error);
        }

        private Dictionary<string, List<string>> ModelStateToDictionary(ModelStateDictionary contextModelState)
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var key in contextModelState.Keys)
            {
                dict[key] = new List<string>();
                foreach (var error in contextModelState[key].Errors)
                {
                    dict[key].Add(error.ErrorMessage);
                }
            }

            return dict;
        }

        #endregion
    }
}