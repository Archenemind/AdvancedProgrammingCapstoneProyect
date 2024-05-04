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
        internal static void ReservationMenu(CarRental.grpc.Reservation.ReservationClient reservation)
        {
            ReservationDTO? createResponse = null;
            string? SelectedOption = null;
            while (SelectedOption != "5")
            {
                Console.Clear();

                Console.WriteLine("Reservation Menu:\n\n");
                Console.WriteLine("Press 1 for creating a reservation");
                Console.WriteLine("Press 2 for getting a reservation");
                Console.WriteLine("Press 3 for updating a reservation");
                Console.WriteLine("Press 4 deleting a reservation");
                Console.WriteLine("Press 5 to go back");

                SelectedOption = Convert.ToString(Console.ReadLine());

                switch (SelectedOption)
                {
                    ///Si se crea otra reservacion 2 reservaciones va a dar error
                    ///ya que no pueden tener el mismo ClientId y VehicleId
                    case "1":
                        createResponse = reservation.CreateReservation(new CreateReservationRequest() { ClientId = 1, VehicleId = 1, EndDate = DateTime.Now.ToString(), Status = StatusTypes.Requested });
                        if (createResponse is null)
                        {
                            Console.WriteLine("Cannot create reservation");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\bCreated new reservation.\b");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        var getResponse = reservation.GetReservation(new GetRequest() { Id = createResponse.Id });
                        if (getResponse.Reservation is null)
                        {
                            Console.WriteLine("Cannot get reservation");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Reservation obtained:\nClientId - {createResponse.ClientId}\nVehicleId - {createResponse.VehicleId}");
                            Console.WriteLine("\nPress a new key to continue");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        createResponse.EndDate = DateTime.Now.ToString();
                        createResponse.Status = StatusTypes.Cancelled;
                        reservation.UpdateReservation(createResponse);

                        var updatedGetResponse = reservation.GetReservation(new GetRequest() { Id = createResponse.Id });
                        if (updatedGetResponse is not null && updatedGetResponse.KindCase == NullableReservationDTO.KindOneofCase.Reservation && updatedGetResponse.Reservation.EndDate == createResponse.EndDate && updatedGetResponse.Reservation.Status == StatusTypes.Cancelled)
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
                        reservation.DeleteReservation(createResponse);
                        var deletedGetResponse = reservation.GetReservation(new GetRequest() { Id = createResponse.Id });
                        if (deletedGetResponse is null || deletedGetResponse.KindCase != NullableReservationDTO.KindOneofCase.Reservation)
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