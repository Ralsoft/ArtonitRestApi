namespace ArtonitRestApi.Models
{
    public class PlaceModel
    {

        [DatabaseName("ID")]
        public int Id { get; set; }

        [DatabaseName("PLACENUMBER")]
        public int PlaceNumber { get; set; }

        [DatabaseName("DESCRIPTION")]
        public string Description { get; set; }

        [DatabaseName("NOTE")]
        public string Note { get; set; }

        [DatabaseName("STATUS")]
        public string Status { get; set; }

        [DatabaseName("NAME")]
        public string Name { get; set; }

        [DatabaseName("ID_PARKING")]
        public int IdParking { get; set; }
    }
}
