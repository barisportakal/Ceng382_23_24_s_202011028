namespace ReservationSystem
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
        IEnumerable<Reservation> GetAllReservations();
    }
}