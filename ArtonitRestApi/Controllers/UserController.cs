using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class UserController : ApiController
    {

        [HttpGet]
        public People UserGetById(string id)
        {
            var query = "select ID_PEP, ID_DB, ID_ORG, SURNAME, NAME, PATRONYMIC," +
                "DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK," +
                 @"NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , FLAG," +
                "LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM " +
                $"from people where ID_PEP = {id}";

            return DatabaseService.Get<People>(query);
        }

        [HttpGet]
        public List<People> UserListGet()
        {
            var query = "select ID_PEP, ID_DB, ID_ORG, SURNAME, NAME, PATRONYMIC," +
                "DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK," +
                @"NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , FLAG," +
                "LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM " +
                $"from people";

            return DatabaseService.GetList<People>(query);
        }

        [HttpPost]
        public string UserAdd([FromBody] People body)
        {
            body.IdDb = 1;
            body.IdArea = 0;

            return DatabaseService.Create(body);
        }

        [HttpPatch]
        public string UserUpdate([FromBody] People body, int id)
        {

            var query = DatabaseService.GenerateUpdateQuery(body, $"ID_PEP={id}");

            return DatabaseService.ExecuteNonQuery(query);
        }


        [HttpDelete]
        public string UserDelete(int id)
        {
            var query = $"delete from person where ID_PEP={id}";

            return DatabaseService.ExecuteNonQuery(query);
        }
    }
}
