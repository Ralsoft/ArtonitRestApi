using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class CardController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CardAdd([FromBody] CardBase body, int userId)
        {
            var verify = AuthService.CheckRightCardAdd();

            if (!verify)
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }

            var card = new CardAdd
            {
                UserId = userId
            };

            card.InitializeFromCardBase(body);

            var result = DatabaseService.Create(card);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, 
                new HttpError(result.ErrorMessage));
        }

        [HttpPost]
        public HttpResponseMessage CardUpdate([FromBody] CardBase body, string OldCardValue)
        {
            var verify = AuthService.CheckRightCardAdd();

            if (!verify)
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }

            var result = DatabaseService.Update(body, $"ID_CARD = '{OldCardValue}'");

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, 
                    new HttpError(result.ErrorMessage));
            }
        }

        public void CardDelete()
        {

        }
    }
}
