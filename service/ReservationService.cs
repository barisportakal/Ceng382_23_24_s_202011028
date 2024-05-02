namespace ReservationSystem
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationHandler reservationHandler;

        public ReservationService(ReservationHandler reservationHandler)
        {
            this.reservationHandler = reservationHandler;
        }

        public void AddReservation(Reservation reservation)
        {
            reservationHandler.AddReservation(reservation);
        }

        public void DeleteReservation(Reservation reservation)
        {
            reservationHandler.DeleteReservation(reservation);
        }

        public void DisplayWeeklySchedule()
        {
            reservationHandler.DisplayWeeklySchedule();
        }

        public List<Reservation> DisplayReservationByReserver(string name)
        {
            return reservationHandler.DisplayReservationByReserver(name);
        }

        public List<Reservation> DisplayReservationByRoomId(string Id)
        {
            return reservationHandler.DisplayReservationByRoomId(Id);
        }
    }
}