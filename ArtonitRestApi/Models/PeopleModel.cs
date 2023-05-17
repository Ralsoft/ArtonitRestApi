using ArtonitRestApi.Annotation;
using System;

namespace ArtonitRestApi.Models
{
    public class People
    {
        [DatabaseName("ID_PEP")]
        private int _id;

        [DatabaseName("ID_DB")]
        private int _idDb;

        [DatabaseName("ID_ORG")]
        private int _org;

        [DatabaseName("SURNAME")]
        private string _surname;

        [DatabaseName("NAME")]
        private string _name;

        [DatabaseName("PATRONYMIC")]
        private string _patronymic;

        [DatabaseName("DATEBIRTH")]
        private DateTime _birth;

        [DatabaseName("PLACELIFE")]
        private string _address;

        [DatabaseName("PLACEREG")]
        private string _registration;

        [DatabaseName("PHONEHOME")]
        private string _homePhone;

        [DatabaseName("PHONECELLULAR")]
        private string _cellularPhone;

        [DatabaseName("PHONEWORK")]
        private string _workPhone;

        [DatabaseName("NUMDOC")]
        private string _documentNum;

        [DatabaseName("DATEDOC")]
        private DateTime _documentDate;

        [DatabaseName("PLACEDOC")]
        private string _documentAddress;

        [DatabaseName("PHOTO")]
        private string _photo;

        [DatabaseName("WORKSTART")]
        private TimeSpan _workStart;

        [DatabaseName("WORKEND")]
        private TimeSpan _workEnd;

        [DatabaseName("ACTIVE")]
        [DataBaseSystemWord]
        private int _active;

        [DatabaseName("FLAG")]
        private int _flag;

        [DatabaseName("LOGIN")]
        private string _login;

        [DatabaseName("PSWD")]
        private string _password;

        [DatabaseName("PEPTYPE")]
        private int _pepType;

        [DatabaseName("POST")]
        private string _post;

        [DatabaseName("PLACEBIRTH")]
        private string _birthAddress;

        [DatabaseName("NOTE")]
        private string _note;

        [DatabaseName("ID_AREA")]
        private int _idArea;

        [DatabaseName("TABNUM")]
        private string _nTab;

        public int Id { get { return _id; } set { _id = value; } }
        public int IdDb { get { return _idDb; } set { _idDb = value; } }
        public int IdOrg { get { return _org; } set { _org = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Patronymic { get { return _patronymic; } set { _patronymic = value; } }
        public DateTime DateBirth { get { return _birth; } set { _birth = value; } }
        public string PlaceLife { get { return _address; } set { _address = value; } }
        public string PlaceReg { get { return _registration; } set { _registration = value; } }
        public string PhoneHome { get { return _homePhone; } set { _homePhone = value; } }
        public string PhoneCellular { get { return _cellularPhone; } set { _cellularPhone = value; } }
        public string PhoneWork { get { return _workPhone; } set { _workPhone = value; } }
        public string NumDoc { get { return _documentNum; } set { _documentNum = value; } }
        public DateTime DateDoc { get { return _documentDate; } set { _documentDate = value; } }
        public string PlaceDoc { get { return _documentAddress; } set { _documentAddress = value; } }
        public string Photo { get { return _photo; } set { _photo = value; } }
        public TimeSpan WorkStart { get { return _workStart; } set { _workStart = value; } }
        public TimeSpan WorkEnd { get { return _workEnd; } set { _workEnd = value; } }
        public int Active { get { return _active; } set { _active = value; } }
        public int Flag { get { return _flag; } set { _flag = value; } }
        public string Login { get { return _login; } set { _login = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public int PepType { get { return _pepType; } set { _pepType = value; } }
        public string Post { get { return _post; } set { _post = value; } }
        public string PlaceBirth { get { return _birthAddress; } set { _birthAddress = value; } }
        public string Note { get { return _note; } set { _note = value; } }
        public int IdArea { get { return _idArea; } set { _idArea = value; } }
        public string TabNum { get { return _nTab; } set { _nTab = value; } }
    }
}
