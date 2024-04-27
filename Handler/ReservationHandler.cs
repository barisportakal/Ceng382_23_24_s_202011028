using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace ReservationSystem
{
    public class ReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger _logger;

        public ReservationHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository, ILogger logger)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }

public void AddReservation(Reservation reservation)
{
    var room = _roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == reservation.Room.RoomId);
    if (room != null)
    {
        room.Capacity--;
        _roomRepository.SaveRooms(_roomRepository.GetRooms());

        var reservations = _reservationRepository.GetAllReservations();
        var list = reservations.ToList();
        list.Add(reservation);
        ((ReservationRepository)_reservationRepository).SaveReservations(list);

        Console.WriteLine(JsonConvert.SerializeObject(new ReservationList { Reservations = list }, Formatting.Indented));
        _logger.Log(new LogRecord(DateTime.Now, reservation.ReserverName, reservation.Room.RoomName));
    }
}

        public void DeleteReservation(Reservation reservation)
        {
            var room = _roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == reservation.Room.RoomId);
            if (room != null)
            {
                room.Capacity++;
                _roomRepository.SaveRooms(_roomRepository.GetRooms());
                _reservationRepository.DeleteReservation(reservation);
                _logger.Log(new LogRecord(DateTime.Now, reservation.ReserverName, reservation.Room.RoomName));
            }
        }

        public void DisplayWeeklySchedule()
        {
            // TO DO: implement weekly schedule display
        }
    }
}