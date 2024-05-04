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
        internal static void VehicleMenu(CarRental.grpc.Car.CarClient car, CarRental.grpc.Motorcycle.MotorcycleClient motorcycle)
        {
            CarDTO? createCarResponse = null;
            MotorcycleDTO? createMotorcycleResponse = null;
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
                        ///Esto va a fallar si en la DB ya hay un vehiculo
                        ///con  PriceId, InsuranceId, SomatonId igual a 1
                        createCarResponse = car.CreateCar(new CreateCarRequest() { CirculationId = 1, Color = 0, Color2 = 0, InsuranceId = 1, SomatonId = 1, PriceId = 1, HasHairConditioner = 0, BrandName = "chevrolet", FabricationDate = "Jan 1, 2009" });
                        if (createCarResponse is null)
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
                        var getResponse = car.GetCar(new GetRequest() { Id = createCarResponse.Id });
                        if (getResponse.Car is null)
                        {
                            Console.WriteLine("Cannot get car");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Car obtained {getResponse.Car.BrandName}");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        createCarResponse.HasHairConditioner = 1;
                        car.UpdateCar(createCarResponse);

                        var updatedGetResponse = car.GetCar(new GetRequest() { Id = createCarResponse.Id });
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
                        car.DeleteCar(createCarResponse);
                        var deletedGetResponse = car.GetCar(new GetRequest() { Id = createCarResponse.Id });
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

                    case "5":
                        ///Esto va a fallar si en la DB ya hay un vehiculo
                        ///con  PriceId, InsuranceId, SomatonId igual a 2
                        createMotorcycleResponse = motorcycle.CreateMotorcycle(new CreateMotorcycleRequest() { CirculationId = 2, Color = 0, Color2 = 0, InsuranceId = 2, SomatonId = 2, PriceId = 2, HasSideCar = 0, BrandName = "Susuki", FabricationDate = "March 1, 2009" });
                        if (createMotorcycleResponse is null)
                        {
                            Console.WriteLine("Cannot create motorcycle");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\bCreated new motorcycle.\b");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "6":
                        var getMotorcycleResponse = motorcycle.GetMotorcycle(new GetRequest() { Id = createMotorcycleResponse.Id });
                        if (getMotorcycleResponse.Motorcycle is null)
                        {
                            Console.WriteLine("Cannot get motorcycle");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Motorcycle obtained {getMotorcycleResponse.Motorcycle.BrandName}");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "7":
                        createMotorcycleResponse.HasSideCar = 1;
                        motorcycle.UpdateMotorcycle(createMotorcycleResponse);

                        var updatedGetMotorcycleResponse = motorcycle.GetMotorcycle(new GetRequest() { Id = createMotorcycleResponse.Id });
                        if (updatedGetMotorcycleResponse is not null && updatedGetMotorcycleResponse.KindCase == NullableMotorcycleDTO.KindOneofCase.Motorcycle && updatedGetMotorcycleResponse.Motorcycle.HasSideCar == 1)
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

                    case "8":
                        motorcycle.DeleteMotorcycle(createMotorcycleResponse);
                        var deletedGetMotorcycleResponse = motorcycle.GetMotorcycle(new GetRequest() { Id = createMotorcycleResponse.Id });
                        if (deletedGetMotorcycleResponse is null || deletedGetMotorcycleResponse.KindCase != NullableMotorcycleDTO.KindOneofCase.Motorcycle)
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