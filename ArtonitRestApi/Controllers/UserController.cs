using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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


        [HttpGet]
        public People UserGetByCard(string card, int cardType)
        {
            var query = $@"select p.ID_PEP, p.ID_DB, p.ID_ORG, p.SURNAME, p.NAME, p.PATRONYMIC, p.DATEBIRTH,
                    p.PLACELIFE, p.PLACEREG, p.PHONEHOME, p.PHONECELLULAR, p.PHONEWORK,
                    p.NUMDOC, p.DATEDOC, p.PLACEDOC, p.PHOTO, p.WORKSTART, p.WORKEND, p.""ACTIVE"" , p.FLAG,
                    p.LOGIN, p.PSWD, p.PEPTYPE, p.POST, p.PLACEBIRTH, p.NOTE, p.ID_AREA, p.TABNUM
                    from people p join card c on c.id_pep = p.id_pep where c.id_card containing '{card}' and c.id_cardtype={cardType};";

            return DatabaseService.Get<People>(query);
        }

        [HttpPost]
        public HttpResponseMessage UserAdd([FromBody] People body)
        {
            body.IdDb = 1;
            body.IdArea = 0;

            var result = DatabaseService.Create(body);

            if(result == "ok")
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                HttpError err = new HttpError(result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        [HttpPatch]
        public HttpResponseMessage UserUpdate([FromBody] People body, int id)
        {

            var query = DatabaseService.GenerateUpdateQuery(body, $"ID_PEP={id}");

            var result = DatabaseService.ExecuteNonQuery(query);

            if (result == "ok")
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                HttpError err = new HttpError(result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }


        [HttpDelete]
        public HttpResponseMessage UserDelete(int id)
        {
            var query = $"delete from person where ID_PEP={id}";

            var result = DatabaseService.ExecuteNonQuery(query);

            if (result == "ok")
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                HttpError err = new HttpError(result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }
    }
}
