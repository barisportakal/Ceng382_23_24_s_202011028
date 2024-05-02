using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace ReservationSystem
{
    public class ReservationHandler
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IRoomRepository roomRepository;
        private readonly ILogger logger;

        public ReservationHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository, ILogger logger)
        {
            this.reservationRepository = reservationRepository;
            this.roomRepository = roomRepository;
            this.logger = logger;
        }

        public void AddReservation(Reservation reservation)
        {
            var room = roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == reservation.Room.RoomId);
            if (room != null)
            {
                room.Capacity--;
                roomRepository.SaveRooms(roomRepository.GetRooms());

                var reservations = reservationRepository.GetAllReservations();
                var list = reservations.ToList();
                list.Add(reservation);
                ((ReservationRepository)reservationRepository).SaveReservations(list);

                //  Console.WriteLine(JsonConvert.SerializeObject(new ReservationList { Reservations = list }, Formatting.Indented));
                logger.Log(new LogRecord(DateTime.Now, reservation.ReserverName, reservation.Room.RoomName));
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            var room = roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == reservation.Room.RoomId);
            if (room != null)
            {
                room.Capacity++;
                roomRepository.SaveRooms(roomRepository.GetRooms());
                reservationRepository.DeleteReservation(reservation);
                logger.Log(new LogRecord(DateTime.Now, reservation.ReserverName, reservation.Room.RoomName));
            }
        }

        public void DisplayWeeklySchedule()
        {
            // sonra burayı yaz.
        }

        public List<Reservation> DisplayReservationByReserver(string name)
        {
            var reservations = reservationRepository.GetAllReservations();
            var reservationsByReserver = reservations.Where(r => r.ReserverName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return reservationsByReserver;
        }
        public List<Reservation> DisplayReservationByRoomId(string Id)
        {
            var reservations = reservationRepository.GetAllReservations();
            var reservationsByRoomId = reservations.Where(r => r.Room.RoomId.Equals(Id, StringComparison.OrdinalIgnoreCase)).ToList();
            return reservationsByRoomId;
        }


    }
}