using backend.Models;
using backend.Repositories.Interfaces;
using backend.Repositories.Services;
using BackEnd.Repositories.Interfaces;
using BackEnd.Repositories.Services;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<userreportsContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IUserInterface, UserService>();
            builder.Services.AddScoped<IVerificationInterface, VerificationService>();
            builder.Services.AddScoped<IReportInterface, ReportService>();
            builder.Services.AddScoped<IConnectionInterface, ConnectionService>();

            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                        builder =>
                        {
                            builder.WithOrigins("*")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                        });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Auth Api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
