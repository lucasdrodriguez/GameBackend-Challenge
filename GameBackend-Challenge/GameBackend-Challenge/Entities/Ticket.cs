namespace GameBackend_Challenge.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; } 


    }
}
