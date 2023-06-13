using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using ArtonitRestApi.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    [Authorize]
    public class PeopleController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage UserGetById(string id)
        {
            var result = PeopleRepository.GetById(id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpGet]
        public HttpResponseMessage UserGetByCard(string card, int? cardType = -1)
        {
            var result = PeopleRepository.GetByCard(card, cardType);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpGet]
        public HttpResponseMessage UserListGet()
        {
           var result = PeopleRepository.GetAll();

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpPost]
        public HttpResponseMessage UserAdd([FromBody] PeopleAdd body)
        {
            var verify = AuthService.CheckRightUserAdd();

            if (!verify)
            {
                return Request.CreateResponse(
                    HttpStatusCode.Unauthorized, 
                    new HttpError("не хватает прав авторизации"));
            }

            var result = PeopleRepository.Add(body);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        [HttpPatch]
        public HttpResponseMessage UserUpdate([FromBody] PeopleAdd body, int id)
        {
            var verify = AuthService.CheckRightUserUpdateDelete(id);

            if (!verify)
            {
                return Request.CreateResponse(
                    HttpStatusCode.Unauthorized, 
                    new HttpError("не хватает прав авторизации"));
            }

            var result= PeopleRepository.Update(body, id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }  
        

        [HttpDelete]
        public HttpResponseMessage UserDelete(int id)
        {
            var verify = AuthService.CheckRightUserUpdateDelete(id);

            if (!verify)
            {
                return Request.CreateResponse(
                    HttpStatusCode.Unauthorized, 
                    new HttpError("не хватает прав авторизации"));
            }

            var result = PeopleRepository.Delete(id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }
    }
}
