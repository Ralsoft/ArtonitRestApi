namespace ArtonitRestApi.Models
{
    [DatabaseName("SSAccessuserBase")]
    public class SSAccessuserBase
    {
        [DatabaseName("ID_PEP")]
        public int? IdPep { get; set; }

        [DatabaseName("ID_ACCESSNAME")]
        public int? IdAccessName { get; set; }
    }


    [DatabaseName("SSAccessuserBase")]
    public class SSAccessuserAdd : SSAccessuserBase
    {
        [DatabaseName("ID_ACCESSUSER")]
        public int? Id { get; set; }

        [DatabaseName("ID_DB")]
        public int? IdDb { get; set; }

        public void InitializeFromSSAccessuserBase(SSAccessuserBase user)
        {
            IdPep = user.IdPep;
            IdAccessName = user.IdAccessName;
        }
    }

}
