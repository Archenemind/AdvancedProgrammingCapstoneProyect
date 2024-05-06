using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Repositories;
using CarRental.Services.Services;

using CarRental.DataAccess.Abstract.Common;
using CarRental.DataAccess.Repositories;
using CarRental.Services.Services;

using CarRental.DataAccess.Abstract.Persons;
using CarRental.DataAccess.Abstract.Vehicles;
using CarRental.DataAccess.Abstract.Reservations;
using CarRental.DataAccess.Abstract.Insurances;
using CarRental.DataAccess.Abstract.Somatons;
using CarRental.DataAccess.Abstract.Circulations;

namespace CarRental.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddSingleton("Data Source=Data.sqlite");
            builder.Services.AddScoped<IPriceRepository, ApplicationRepository>();
            builder.Services.AddScoped<IPersonRepository, ApplicationRepository>();
            builder.Services.AddScoped<IVehicleRepository, ApplicationRepository>();
            builder.Services.AddScoped<IReservationRepository, ApplicationRepository>();
            builder.Services.AddScoped<IInsuranceRepository, ApplicationRepository>();
            builder.Services.AddScoped<ISomatonRepository, ApplicationRepository>();    
            builder.Services.AddScoped<ICirculationRepository, ApplicationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<PriceService>();
            app.MapGet("/price", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.MapGrpcService<ClientService>();
            app.MapGet("/client", () => "");

            app.MapGrpcService<CarService>();
            app.MapGet("/car", () => "");

            app.MapGrpcService<MotorcycleService>();
            app.MapGet("/motorcycle", () => "");

            app.MapGrpcService<ReservationService>();
            app.MapGet("/reservation", () => "");

            app.MapGrpcService<InsuranceService>();
            app.MapGet("/insurance", () => "");

            app.MapGrpcService<SomatonService>();
            app.MapGet("/somaton", () => "");

            app.MapGrpcService<CirculationService>();
            app.MapGet("/circulation", () => "");
            app.Run();
        }
    }
}