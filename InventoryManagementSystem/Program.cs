using IMSBusinessLogic;
using IMSBusinessLogic.MediatR.Handlers;
using IMSDataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connStr = builder.Configuration.GetConnectionString("default");
            builder.Services.AddDbContext<ProductDbContext>(opt =>
            {
                opt.UseSqlServer(connStr);
            });
            builder.Services.AddScoped<IInventory, Inventory>();
            
            builder.Services.AddScoped<IEmail,Email>();
            builder.Services.AddMediatR(typeof(GetAllProductsHandler).Assembly);
            builder.Services.AddMediatR(typeof(GetByIdHandler).Assembly);
            builder.Services.AddMediatR(typeof(CreateHandler).Assembly);
            builder.Services.AddMediatR(typeof(UpdateHandler).Assembly);
            builder.Services.AddMediatR(typeof(RecordSaleHandler).Assembly);
            builder.Services.AddMediatR(typeof(GenerateReportHandler).Assembly);

            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
