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
        internal static void ClientMenu(CarRental.grpc.Client.ClientClient client)
        {
            ClientDTO? createResponse = null;
            string? SelectedOption = null;
            while (SelectedOption != "5")
            {
                Console.Clear();

                Console.WriteLine("Client Menu:\n\n");
                Console.WriteLine("Press 1 for creating a client");
                Console.WriteLine("Press 2 for getting a client");
                Console.WriteLine("Press 3 for updating a client");
                Console.WriteLine("Press 4 deleting a client");
                Console.WriteLine("Press 5 to go back");

                SelectedOption = Convert.ToString(Console.ReadLine());

                switch (SelectedOption)
                {
                    case "1":
                        createResponse = client.CreateClient(new CreateClientRequest() { Name = "Carlos", LastName = "Garcia", CI = "123456789", CountryName = "Cuba", Phone = "12345645676" });
                        if (createResponse is null)
                        {
                            Console.WriteLine("Cannot create client");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\bCreated new client.\b");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        var getResponse = client.GetClient(new GetRequest() { Id = 1 });
                        if (getResponse.Client is null)
                        {
                            Console.WriteLine("Cannot get client");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Client obtained {getResponse.Client.Name} {getResponse.Client.LastName}");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        createResponse.CountryName = "USA";
                        createResponse.Phone = "01";
                        client.UpdateClient(createResponse);

                        var updatedGetResponse = client.GetClient(new GetRequest() { Id = createResponse.Id });
                        if (updatedGetResponse is not null && updatedGetResponse.KindCase == NullableClientDTO.KindOneofCase.Client && updatedGetResponse.Client.CountryName == "USA" && updatedGetResponse.Client.Phone == "01")
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
                        client.DeleteClient(createResponse);
                        var deletedGetResponse = client.GetClient(new GetRequest() { Id = createResponse.Id });
                        if (deletedGetResponse is null || deletedGetResponse.KindCase != NullableClientDTO.KindOneofCase.Client)
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