
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
            builder.Services.AddTransient<IRepository<Employees>, EmployeeRepository>();
            builder.Services.AddTransient<IRepository<Managers>, ManagersRepository>();
            builder.Services.AddTransient<IRepository<Departments>, DepartmentsRepository>();
            builder.Services.AddScoped<IEmployeesLogic, EmployeeLogic>();
            builder.Services.AddScoped<IManagerLogic, ManagerLogic>();
            builder.Services.AddScoped<IMixedLogic, MixedLogic>();
            builder.Services.AddScoped<IImportLogic, ImportLogic>();
            builder.Services.AddScoped<IExportLogic, ExportLogic>();
            builder.Services.AddScoped<IDepartmentLogic, DepartmentLogic>();


            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HSZFT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
             {
             options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
             });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HSZFT", Version = "v1" });
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
