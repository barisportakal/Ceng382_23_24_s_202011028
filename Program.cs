using System;
using System.Linq;

namespace ReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var roomRepository = new RoomRepository("Data.json");
            var reservationRepository = new ReservationRepository("reservation.json");
            var logger = new FileLogger("log.json");
            var reservationHandler = new ReservationHandler(reservationRepository, roomRepository, logger);

            Console.WriteLine("Select an operation:");
            Console.WriteLine("1: Add Reservation");
            Console.WriteLine("2: Display All Reservations");
            Console.WriteLine("3: Display Reservations by Reserver Name");
            Console.WriteLine("4: Display Reservations by Room ID");
            Console.WriteLine("5: Delete Reservation by Room ID");
            Console.WriteLine("6: Get Logs by Reserver Name");
            Console.WriteLine("7: Display Logs with Date Filter");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var room = roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == "016");
                    var reservationAddData = new Reservation(DateTime.Now, DateTime.Today, "emin baris portakal", room);
                    reservationHandler.AddReservation(reservationAddData);
                    break;
                case 2:
                    var reservations = reservationRepository.GetAllReservations();
                    foreach (var reservation in reservations)
                    {
                        Console.WriteLine($"Reservation: {reservation.ReserverName} on {reservation.Date} at {reservation.Time} in {reservation.Room.RoomName}");
                    }
                    break;
                case 3:
                    var findReservationsByReserverName = reservationHandler.DisplayReservationByReserver("emin baris portakal");
                    foreach (var reservation in findReservationsByReserverName)
                    {
                        Console.WriteLine($"Reservation: {reservation.ReserverName} on {reservation.Date} at {reservation.Time} in {reservation.Room.RoomName}");
                    }
                    break;
                case 4:
                    var findReservationsByRoomId = reservationHandler.DisplayReservationByRoomId("015");
                    foreach (var reservation in findReservationsByRoomId)
                    {
                        Console.WriteLine($"Reservation: {reservation.ReserverName} on {reservation.Date} at {reservation.Time} in {reservation.Room.RoomName}");
                    }
                    break;
                case 5:
                    var reservationDelete = reservationRepository.GetAllReservations().FirstOrDefault(r => r.Room.RoomId == "015");
                    reservationHandler.DeleteReservation(reservationDelete);
                    break;
                case 6:
                    var logs = logger.GetLogsWithReserverName("emin baris portakal");
                    foreach (var log in logs)
                    {
                        Console.WriteLine($"{log.Timestamp} - {log.ReserverName} - {log.RoomName}");
                    }
                    break;
                case 7:
                    DateTime startDate = DateTime.Parse("2024-04-25T00:00:00.000+03:00");
                    DateTime endDate = DateTime.Parse("2024-04-26T00:00:00.000+03:00");
                    var dateFilteredLogs = logger.DisplayLogs(startDate, endDate);
                    foreach (var log in dateFilteredLogs)
                    {
                        Console.WriteLine($"Timestamp: {log.Timestamp}, ReserverName: {log.ReserverName}, RoomName: {log.RoomName}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
