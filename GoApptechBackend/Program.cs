
using GoApptechBackend.Data;
using GoApptechBackend.Models;
using GoApptechBackend.Repository.Irepository;
using GoApptechBackend.Repository;
using Microsoft.EntityFrameworkCore;
using GoApptechBackend.MappingConfig;

namespace GoApptechBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingConfig.MappingConfig));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
