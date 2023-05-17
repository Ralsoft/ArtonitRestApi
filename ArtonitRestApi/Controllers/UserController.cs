using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost]
        public string UserAdd([FromBody] People body)
        {
            body.IdDb = 1;
            body.IdArea = 0;

            return DatabaseService.Create(body);
        }


        [HttpPost]
        public string UserAddQuery([FromBody] People body)
        {
            body.IdDb = 1;
            body.IdArea = 0;

            return DatabaseService.GenerateCreateQuery(body);
        }
    }
}
