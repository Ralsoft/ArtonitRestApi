using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class SSAccessUserController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage UserAccessGet()
        {
            var result = SSAccessUserRepository.GetAll();

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpGet]
        public HttpResponseMessage UserAccessGet(string id) 
        {
            var result = SSAccessUserRepository.GetById(id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpPost]
        public HttpResponseMessage UserAccessAdd([FromBody] SSAccessuserBase body)
        {
          
            var result = SSAccessUserRepository.Add(body);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }

      
        [HttpDelete]
        public HttpResponseMessage UserAccessDel(int userId, int accessnameId)
        {

            var result = SSAccessUserRepository.Delete(userId, accessnameId);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }
    }
}
