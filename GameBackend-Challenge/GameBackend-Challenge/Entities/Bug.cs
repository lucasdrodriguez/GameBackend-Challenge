namespace GameBackend_Challenge.Entities
{
    public class Bug : Ticket
    {

        public DateTime ResolutionTime { get; set; } // bug
        public int SeverityLvl { get; set; } // bug 
        public int Status { get; set; } // bug 
        public string Title { get; set; } // ambos
        public Attachement Attachement { get; set; } // bug 
    }
}