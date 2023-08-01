using Microsoft.EntityFrameworkCore;
using WebApplicationZno.Persistance;
using Npgsql.EntityFrameworkCore;

namespace WebApplicationZno
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");
            // Add services to the container.
            builder.Services.AddDbContext<EfDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            builder.Services.AddScoped<IQuestionRepository, EfQuestionRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}