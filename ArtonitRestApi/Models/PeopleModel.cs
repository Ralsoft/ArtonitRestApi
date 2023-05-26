using ArtonitRestApi.Annotation;
using System;

namespace ArtonitRestApi.Models
{
    public class People
    {

        [DatabaseName("ID_PEP")]
        public int? Id { get; set; }

        [DatabaseName("ID_DB")]
        public int? IdDb { get; set; }
        
        [DatabaseName("ID_ORG")]
        public int? IdOrg { get; set; }

        [DatabaseName("SURNAME")]
        public string Surname { get; set; }

        [DatabaseName("NAME")]
        public string Name { get; set; }

        [DatabaseName("PATRONYMIC")]
        public string Patronymic { get; set; }

        [DatabaseName("DATEBIRTH")]
        public DateTime DateBirth { get; set; }

        [DatabaseName("PLACELIFE")]
        public string PlaceLife { get; set; }

        [DatabaseName("PLACEREG")]
        public string PlaceReg { get; set; }

        [DatabaseName("PHONEHOME")]
        public string PhoneHome { get; set; }

        [DatabaseName("PHONECELLULAR")]
        public string PhoneCellular { get; set; }

        [DatabaseName("PHONEWORK")]
        public string PhoneWork { get; set; }

        [DatabaseName("NUMDOC")]
        public string NumDoc { get; set; }

        [DatabaseName("DATEDOC")]
        public DateTime DateDoc { get; set; }

        [DatabaseName("PLACEDOC")]
        public string PlaceDoc { get; set; }

        [DatabaseName("PHOTO")]
        public string Photo { get; set; }

        [DatabaseName("WORKSTART")]
        public TimeSpan WorkStart { get; set; }

        [DatabaseName("WORKEND")]
        public TimeSpan WorkEnd { get; set; }

        [DatabaseName("ACTIVE")]
        [DataBaseSystemWord]
        public int Active { get; set; }

        [DatabaseName("FLAG")]
        public int? Flag { get; set; }

        [DatabaseName("LOGIN")]
        public string Login { get; set; }

        [DatabaseName("PSWD")]
        public string Password { get; set; }

        [DatabaseName("PEPTYPE")]
        public int? PepType { get; set; }

        [DatabaseName("POST")]
        public string Post { get; set; }

        [DatabaseName("PLACEBIRTH")]
        public string PlaceBirth { get; set; }

        [DatabaseName("NOTE")]
        public string Note { get; set; }

        [DatabaseName("ID_AREA")]
        public int? IdArea { get; set; }

        [DatabaseName("TABNUM")]
        public string TabNum { get; set; }
    }
}
