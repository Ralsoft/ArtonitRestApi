namespace ArtonitRestApi.Models
{
    public class User
    {
        [DatabaseName("id_pep")]
        public int Id { get; set; }

        [DatabaseName("flag")]
        public string Flag { get; set; }

        [DatabaseName("id_orgctrl")]
        public int IdOgrCtrl { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
