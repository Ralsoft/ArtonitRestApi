using ArtonitRestApi.Annotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;


namespace ArtonitRestApi.Models
{
    [DatabaseName("Card")]
    public class CardModel
    {
        [DatabaseName("ID_CARDTYPE")]
        public int? CardType { get; set; }

        [DatabaseName("ID_CARD")]
        public string CardValue { get; set; }

        [DatabaseName("ACTIVE")]
        [DataBaseSystemWord]
        public int? Active { get; set; }

        [DatabaseName("TIMESTART")]
        public DateTime DateStart { get; set; }

        [DatabaseName("TIMEEND")]
        public DateTime DateStop { get; set; }
    }

    public class Card : CardModel
    {
        [DatabaseName("ID_PEP")]
        public int UserId { get; set; }

        public Card(CardModel model)
        {
            CardValue = model.CardValue;
            CardType = model.CardType;
            Active = model.Active;
            DateStart = model.DateStart;
            DateStop = model.DateStop;  
        }
    }
}
