using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities.Types;
using Grpc.Net.Client;
using System.Net;
using CarRental.grpc;
using System.Threading.Channels;

namespace CarRental.ConsoleApp
{
    internal partial class Program
    {
        internal static void PriceMenu(CarRental.grpc.Price.PriceClient client)
        {
            PriceDTO? createResponse = null;
            string? SelectedOption = null;
            while (SelectedOption != "5")
            {
                Console.Clear();

                Console.WriteLine("Price Menu:\n\n");
                Console.WriteLine("Press 1 for creating a price");
                Console.WriteLine("Press 2 for getting a price");
                Console.WriteLine("Press 3 for updating a price");
                Console.WriteLine("Press 4 deleting a price");
                Console.WriteLine("Press 5 to go back");

                SelectedOption = Convert.ToString(Console.ReadLine());

                switch (SelectedOption)
                {
                    case "1":
                        createResponse = client.CreatePrice(new CreatePriceRequest() { Value = 5, MoneyType = MoneyTypes.Mlc });
                        if (createResponse is null)
                        {
                            Console.WriteLine("Cannot create price");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\bCreated new price.\b");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        var getResponse = client.GetPrice(new GetRequest() { Id = 1 });
                        if (getResponse.Price is null)
                        {
                            Console.WriteLine("Cannot get price");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Price obtained {getResponse.Price.Value} {getResponse.Price.MoneyType.ToString()}");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        createResponse.Value = 20000;
                        client.UpdatePrice(createResponse);

                        var updatedGetResponse = client.GetPrice(new GetRequest() { Id = createResponse.Id });
                        if (updatedGetResponse is not null && updatedGetResponse.KindCase == NullablePriceDTO.KindOneofCase.Price && updatedGetResponse.Price.Value == 20000)
                        {
                            Console.WriteLine("Successfully modified");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Could not update :(");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        client.DeletePrice(createResponse);
                        var deletedGetResponse = client.GetPrice(new GetRequest() { Id = createResponse.Id });
                        if (deletedGetResponse is null || deletedGetResponse.KindCase != NullablePriceDTO.KindOneofCase.Price)
                        {
                            Console.WriteLine("Successfully deleted");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Could not delete");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }

                        break;

                    case "5":
                        break;

                    default:
                        Console.Write("\nWrong key");
                        break;
                }
            }
        }
    }
}