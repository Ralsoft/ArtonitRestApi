using System;

namespace ArtonitRestApi.Models
{
    public class GarageModel
    {      
        public int Id { get; set; }
  
        public string Name { get; set; } 

        public DateTime DateStart { get; set; }

        public int NotCount { get; set; }
    }
}
