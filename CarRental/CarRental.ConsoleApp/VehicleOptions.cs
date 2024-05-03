using CarRental.grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.ConsoleApp
{
    internal partial class Program
    {
        internal static void VehicleMenu(CarRental.grpc.Car.CarClient car)
        {
            CarDTO? createResponse = null;
            string? SelectedOption = null;
            while (SelectedOption != "9")
            {
                Console.Clear();

                Console.WriteLine("Vehicle Menu:\n\n");
                Console.WriteLine("Press 1 for creating a car");
                Console.WriteLine("Press 2 for getting a car");
                Console.WriteLine("Press 3 for updating a car");
                Console.WriteLine("Press 4 deleting a car");
                Console.WriteLine("Press 5 for creating a motorcycle");
                Console.WriteLine("Press 6 for getting a motorcycle");
                Console.WriteLine("Press 7 for updating a motorcycle");
                Console.WriteLine("Press 8 deleting a motorcycle");
                Console.WriteLine("Press 9 to go back");

                SelectedOption = Convert.ToString(Console.ReadLine());

                switch (SelectedOption)
                {
                    case "1":
                        createResponse = car.CreateCar(new CreateCarRequest() { CirculationId = 1, Color = 0, Color2 = 0, InsuranceId = 1, SomatonId = 1, PriceId = 1, HasHairConditioner = 0, BrandName = "chevrolet", FabricationDate = "Jan 1, 2009" });
                        if (createResponse is null)
                        {
                            Console.WriteLine("Cannot create car");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\bCreated new car.\b");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        var getResponse = car.GetCar(new GetRequest() { Id = 1 });
                        if (getResponse.Car is null)
                        {
                            Console.WriteLine("Cannot get car");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Car obtained");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        createResponse.HasHairConditioner = 1;
                        car.UpdateCar(createResponse);

                        var updatedGetResponse = car.GetCar(new GetRequest() { Id = createResponse.Id });
                        if (updatedGetResponse is not null && updatedGetResponse.KindCase == NullableCarDTO.KindOneofCase.Car && updatedGetResponse.Car.HasHairConditioner == 1)
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
                        car.DeleteCar(createResponse);
                        var deletedGetResponse = car.GetCar(new GetRequest() { Id = createResponse.Id });
                        if (deletedGetResponse is null || deletedGetResponse.KindCase != NullableCarDTO.KindOneofCase.Car)
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

                    case "9":
                        break;

                    default:
                        Console.Write("\nWrong key");
                        break;
                }
            }
        }
    }
}