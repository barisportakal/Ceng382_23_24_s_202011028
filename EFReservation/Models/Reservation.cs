namespace EFReservation
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = default!;
        
        public string UserId { get; set; } = default!;
}
}
