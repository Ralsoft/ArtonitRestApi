namespace ArtonitRestApi.Models
{
    public class Ss_accessuser
    {
        [DatabaseName("ID_ACCESSUSER")]
        public int? Id { get; set; }

        [DatabaseName("ID_DB")]
        public int? IdDb { get; set; }

        [DatabaseName("ID_PEP")]
        public int? IdPep { get; set; }

        [DatabaseName("ID_ACCESSNAME")]
        public int? IdAccessName { get; set; }
    }
}
