using ArtonitRestApi.Annotation;
using DocumentFormat.OpenXml.EMMA;
using System;

namespace ArtonitRestApi.Models
{
    [DatabaseName("Card")]
    public class CardBase
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

    [DatabaseName("Card")]
    public class CardAdd : CardBase
    {
        
        public int CardId { get; set; }

        [DatabaseName("ID_PEP")]
        public int UserId { get; set; }

        public void InitializeFromCardBase(CardBase model)
        {
            CardValue = model.CardValue;
            CardType = model.CardType;
            Active = model.Active;
            DateStart = model.DateStart;
            DateStop = model.DateStop;
        }
    }
}
