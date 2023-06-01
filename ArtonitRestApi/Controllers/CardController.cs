using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class CardController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CardAdd([FromBody] CardBase body, int userId)
        {

            var verify = AuthService.CheckRightCardAdd(userId);

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

        [HttpPost]
        public HttpResponseMessage CardUpdate([FromBody] CardBase body, string OldCardValue)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idOrgCtrl = userIdentity?.FindFirst(MyClaimTypes.IdOrgCtrl)?.Value;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var verifyQuery = $@"select p.id_pep from people p 
                join organization_getchild (1, {idOrgCtrl}) og on og.id_org=p.id_org
                where p.id_pep={idPep}";

            var verify = DatabaseService.Get<RightsVerificationModel>(verifyQuery);

            if (verify.Id != Convert.ToInt32(idPep))
            {
                HttpError err = new HttpError("не хватает прав авторизации");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, err);
            }

            var updateQuery = DatabaseService.GenerateUpdateQuery(body, $"ID_CARD = '{OldCardValue}'");

            var result = DatabaseService.ExecuteNonQuery(updateQuery);

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

        public void CardDelete()
        {

        }
    }
}
