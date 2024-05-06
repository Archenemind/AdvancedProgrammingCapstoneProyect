using CarRental.Domain.Entities.Types;
using Grpc.Net.Client;
using System.Net;
using CarRental.grpc;
using System.Threading.Channels;

namespace CarRental.ConsoleApp
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5051", new GrpcChannelOptions { HttpHandler = httpHandler });
            if (channel is null)
            {
                Console.WriteLine("Cannot connect");
                Console.WriteLine("Press a key to continue");
                Console.ReadKey();
                channel.Dispose();
                return;
            }

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Menu:\n\n");
                Console.WriteLine("Press 1 for price options");
                Console.WriteLine("Press 2 for vehicle options");
                Console.WriteLine("Press 3 for client options");
                Console.WriteLine("Press 4 for reservation options");
                Console.WriteLine("Press 5 to exit\n");

                string? option = Convert.ToString(Console.ReadLine());

                switch (option)
                {
                    case "1":
                        CarRental.grpc.Price.PriceClient priceClient = new CarRental.grpc.Price.PriceClient(channel);
                        PriceMenu(priceClient);
                        break;

                    case "2":
                        CarRental.grpc.Somaton.SomatonClient somatonClient = new CarRental.grpc.Somaton.SomatonClient(channel);
                        CarRental.grpc.Insurance.InsuranceClient insuranceClint = new CarRental.grpc.Insurance.InsuranceClient(channel);
                        CarRental.grpc.Circulation.CirculationClient circulationClient = new CarRental.grpc.Circulation.CirculationClient(channel);
                        CarRental.grpc.Car.CarClient carClient = new CarRental.grpc.Car.CarClient(channel);

                        CarRental.grpc.Motorcycle.MotorcycleClient motorcycleClient = new CarRental.grpc.Motorcycle.MotorcycleClient(channel);

                        VehicleMenu(carClient, motorcycleClient,somatonClient,insuranceClint,circulationClient);
                        break;

                    case "3":
                        CarRental.grpc.Client.ClientClient clientClient = new CarRental.grpc.Client.ClientClient(channel);
                        ClientMenu(clientClient);
                        break;

                    case "4":
                        CarRental.grpc.Reservation.ReservationClient reservationClient = new CarRental.grpc.Reservation.ReservationClient(channel);
                        ReservationMenu
                            (reservationClient);
                        break;

                    case "5":
                        Console.WriteLine("Goodbye :(");
                        channel.Dispose();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nWrong key");

                        break;
                }
            }
        }
    }
}