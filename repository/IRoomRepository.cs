namespace ReservationSystem{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        void SaveRooms(IEnumerable<Room> rooms);
    }
}