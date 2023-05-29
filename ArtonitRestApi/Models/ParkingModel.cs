﻿using ArtonitRestApi.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;


namespace ArtonitRestApi.Models
{

    public class ParkingModel
    {

      

        public int id { get; set; }

  
        public string Name { get; set; } 


        public DateTime created { get; set; }


        public int parent { get; set; }
    }



   /*
    public class Place : PlaceModel
    {
        [DatabaseName("ID")]
        public int UserId { get; set; }

        public Card(CardModel model)
        {
            Id = model.Id;
            Description = model.Description;
            Note = model.Note;
            Status = model.Status;
            Name = model.Name;
            ID_parkong = model.ID_parkong;

        }
    }
   */
}
