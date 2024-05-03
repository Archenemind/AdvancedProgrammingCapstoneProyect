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
                        CarRental.grpc.Price.PriceClient client = new CarRental.grpc.Price.PriceClient(channel);
                        PriceMenu(client);
                        break;

                    case "2":
                        CarRental.grpc.Car.CarClient carClient = new CarRental.grpc.Car.CarClient(channel);

                        VehicleMenu(carClient);
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