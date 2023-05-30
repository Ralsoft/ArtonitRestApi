using System;

namespace ArtonitRestApi.Models
{

    public class ParkingModel
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public DateTime Created { get; set; }

        public int Parent { get; set; }
    }
}
