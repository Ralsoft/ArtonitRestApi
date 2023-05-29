using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class SSAccessUserController : ApiController
    {
        
        
        [HttpGet]
        public List<Ss_accessuser> UserAccessGet()
        {
            var query = $@"select ID_ACCESSUSER, ID_DB, ID_PEP, ID_ACCESSNAME from ss_accessuser";

            return DatabaseService.GetList<Ss_accessuser>(query);
        } 
        
        [HttpGet]
        public Ss_accessuser UserAccessGet(string id)
        {
            var query = $@"select ID_ACCESSUSER, ID_DB, ID_PEP, ID_ACCESSNAME from ss_accessuser where ID_ACCESSUSER={id}";

            return DatabaseService.Get<Ss_accessuser>(query);
        }


        [HttpPost]
        public HttpResponseMessage UserAccessAdd([FromBody] Ss_accessuser body)
        {
            body.IdDb = 1;

            var result = DatabaseService.Create(body);

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
        public HttpResponseMessage UserAccessDel(int userId, int accessnameId)
        {
            var query = $"delete from ss_accessuser ssa where ssa.id_pep = {userId} and ssa.id_accessname={accessnameId}";

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
