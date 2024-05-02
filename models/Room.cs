namespace ReservationSystem
{
    public class Room
    {
        public string RoomId { get; }
        public string RoomName { get; } //immutable.
        public int Capacity { get; set; } //sadee bunu mutable yaptım değiştirmem gerekebilir.

        public Room(string roomId, string roomName, int capacity)
        {
            this.RoomId = roomId;
            this.RoomName = roomName;
            this.Capacity = capacity;
        }
    }
}