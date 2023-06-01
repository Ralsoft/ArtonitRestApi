namespace ArtonitRestApi.Models
{
    public class EventModel
    {
        [DatabaseName("event_code")]
        public string EventCode { get; set; }

        [DatabaseName("event_time")]
        public string EventTime { get; set; }

        [DatabaseName("created")]
        public string Created { get; set; }

        [DatabaseName("is_enter")]
        public string IsEnter { get; set; }

        [DatabaseName("id_parking")]
        public string IdParking { get; set; }
    }
}
