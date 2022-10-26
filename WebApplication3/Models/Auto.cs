namespace WebApplication3.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Cost { get; set; }
        public string BodyType { get; set; }
        public int EngineVolume { get; set; }
        public bool ClearedByCustomer { get; set; }
        public string Comment { get; set; }
    }
}
