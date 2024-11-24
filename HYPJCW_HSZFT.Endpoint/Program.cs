
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Repository;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Data;

namespace HYPJCW_HSZFT.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IEmployeesLogic, EmployeeLogic>();
            builder.Services.AddScoped<IEmployeesLogic, EmployeeLogic>();
            builder.Services.AddScoped<IRepository<Employees>, Repository<Employees>>();
            builder.Services.AddScoped<IRepository<Managers>, Repository<Managers>>();
            builder.Services.AddScoped<IRepository<Departments>, Repository<Departments>>();
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HSZFT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                options.UseLazyLoadingProxies();
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WaterLevel Api", Version = "v1" });
            });

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
