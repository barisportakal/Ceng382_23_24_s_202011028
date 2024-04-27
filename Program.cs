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

            var room = roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == "016");
            var reservationAddData = new Reservation(DateTime.Now, DateTime.Today, "baris portakal", room);
            reservationHandler.AddReservation(reservationAddData);

            var reservations = reservationRepository.GetAllReservations();
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation: {reservation.ReserverName} on {reservation.Date} at {reservation.Time} in {reservation.Room.RoomName}");
            }

           // var reservationDelete = reservationRepository.GetAllReservations().FirstOrDefault(r => r.Room.RoomId == "001");
            //reservationHandler.DeleteReservation(reservationDelete);

        }
    }
}