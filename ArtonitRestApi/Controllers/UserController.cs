using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {

        [HttpGet]
        public People UserGetById(string id)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select ID_PEP, ID_DB, p.ID_ORG, SURNAME, p.NAME, PATRONYMIC,
                DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK,
                NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , p.FLAG,
                LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM
                from people p
                join organization_getchild (1, (select p2. id_orgctrl from people p2 where p2.id_pep={idPep})) og on p.id_org=og.id_org
                where p.ID_PEP = {id}";

            return DatabaseService.Get<People>(query);
        }                   

        [HttpGet]
        public People UserGetByCard(string card, int cardType)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select p.ID_PEP, p.ID_DB, p.ID_ORG, p.SURNAME, p.NAME, p.PATRONYMIC, p.DATEBIRTH,
                    p.PLACELIFE, p.PLACEREG, p.PHONEHOME, p.PHONECELLULAR, p.PHONEWORK,
                    p.NUMDOC, p.DATEDOC, p.PLACEDOC, p.PHOTO, p.WORKSTART, p.WORKEND, p.""ACTIVE"" , p.FLAG,
                    p.LOGIN, p.PSWD, p.PEPTYPE, p.POST, p.PLACEBIRTH, p.NOTE, p.ID_AREA, p.TABNUM
                    from people p
                    join card c on c.id_pep = p.id_pep
                    join organization_getchild (1, (select p2. id_orgctrl from people p2 where p2.id_pep={idPep})) 
                    og on p.id_org=og.id_org where c.id_card containing '{card}' and c.id_cardtype={cardType};";

            return DatabaseService.Get<People>(query);
        }

        [HttpGet]
        public List<People> UserListGet()
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select ID_PEP, ID_DB, ID_ORG, SURNAME, NAME, PATRONYMIC,
                DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK,
                NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , FLAG,
                LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM
                from people p
                where p.id_org in (select id_org from 
                organization_getchild (1, (select p2.id_orgctrl from people p2 where p2.id_pep={idPep})))";

            return DatabaseService.GetList<People>(query);
        }

        [HttpPost]
        public HttpResponseMessage UserAdd([FromBody] People body)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idOrgCtrl = userIdentity?.FindFirst(MyClaimTypes.IdOrgCtrl)?.Value;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select og.id_org from organization_getchild 
                (1, (select p. id_orgctrl from people p where p.id_pep={idPep})) 
                og where og.id_org={idOrgCtrl}";

            var verify = DatabaseService.Get<RightsVerificationModel>(query);

            if (verify.Id == 0)
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }
            var rdbDatabase = DatabaseService.Get<RDBDatabase>("select GEN_ID (gen_people_id, 1) from RDB$DATABASE");

            body.Id = rdbDatabase.Id;
            body.IdDb = 1;
            body.IdArea = 0;

            var defaultDateTime = new DateTime(0);

            if (body.DateBirth == defaultDateTime) body.DateBirth = DateTime.Now;
            if (body.DateDoc == defaultDateTime) body.DateDoc = DateTime.Now;
            if(body.IdOrg == 0) body.IdOrg = Convert.ToInt32(idOrgCtrl);

            var result = DatabaseService.Create(body);

            if(result == "ok")
            {
                return Request.CreateResponse(HttpStatusCode.OK, rdbDatabase.Id);
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
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var queryVerify = $@"select p2.id_pep from people p2 
                join organization_getchild (1, (select p. id_orgctrl from people p where p.id_pep={idPep})) 
                og on p2.id_org=og.id_org where p2.id_pep={id}";

            var verify = DatabaseService.Get<RightsVerificationModel>(queryVerify);

            if (verify.Id == 0)
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }


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
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var queryVerify = $@"select p2.id_pep from people p2
                join organization_getchild (1, (select p.id_orgctrl from people p where p.id_pep={idPep})) 
                og on p2.id_org=og.id_org where p2.id_pep={id}";

            var verify = DatabaseService.Get<RightsVerificationModel>(queryVerify);

            if (verify.Id == 0)
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }

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
