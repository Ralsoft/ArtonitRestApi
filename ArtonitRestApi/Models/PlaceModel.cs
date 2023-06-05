namespace ArtonitRestApi.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }

        public int PlaceNumber { get; set; }

        public string Description { get; set; } 

        public string Note { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }

        public int IdParking { get; set; }
    }
}
