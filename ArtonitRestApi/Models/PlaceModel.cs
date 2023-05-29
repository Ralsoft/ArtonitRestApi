namespace ArtonitRestApi.Models
{
    public class PlaceModel
    {
      //  [DatabaseName("ID")]
        public string Id { get; set; }

        public string PlaceNumber { get; set; }

        //public string IdCounters { get; set; }

        //[DatabaseName("NAME")]
        public string Description { get; set; } 

        //[DatabaseName("NAME")]
        public string Note { get; set; }

        //[DatabaseName("NAME")]
        public string Status { get; set; }

        //[DatabaseName("NAME")]
        public string Name { get; set; }

        //[DatabaseName("ID")]
        public string ID_parking { get; set; }
    }
}
