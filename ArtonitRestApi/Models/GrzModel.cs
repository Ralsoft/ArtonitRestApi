namespace ArtonitRestApi.Models
{
    public class GrzModel
    {
        [DatabaseName("ID_CARD")]
        public string IdCard { get; set; }

        [DatabaseName("id_pep")]
        public string IdPep { get; set; }

        [DatabaseName("timestart")]
        public string Timestart { get; set; }

        [DatabaseName("timeend")]
        public string Timeend { get; set; }

        [DatabaseName("note")]
        public string Note { get; set; }

        [DatabaseName("status")]
        public string Status { get; set; }

        [DatabaseName("ACTIVE")]
        public string Active { get; set; }

        [DatabaseName("flag")]
        public string Flag { get; set; }

        [DatabaseName("id_cardtype")]
        public string IdCardType { get; set; }
    }
}
