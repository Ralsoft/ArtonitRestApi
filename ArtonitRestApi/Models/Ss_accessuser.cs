namespace ArtonitRestApi.Models
{
    public class Ss_accessuser
    {
        [DatabaseName("ID_ACCESSUSER")]
        private int _id;

        [DatabaseName("ID_DB")]
        private int _idDb;

        [DatabaseName("ID_PEP")]
        private int _idPep;

        [DatabaseName("ID_ACCESSNAME")]
        private int _idAccessName;

        public int Id { get { return _id; } set { _id = value; } }
        public int IdDb { get { return _idDb; } set { _idDb = value; } }

        public int IdPep { get { return _idPep; } set { _idPep = value; } }
        public int IdAccessName { get { return _idAccessName; } set { _idAccessName = value; } }
    }
}
