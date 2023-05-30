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
        public People UserGetById(string id)
        {
            return PeopleRepository.GetById(id);
        }


        [HttpGet]
        public People UserGetByCard(string card, int? cardType = -1)
        {
            return PeopleRepository.GetByCard(card, cardType);
        }


        [HttpGet]
        public List<People> UserListGet()
        {
           return PeopleRepository.GetAll();
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

            if (result > 0)
                return Request.CreateResponse(HttpStatusCode.OK, result);

            return Request.CreateResponse(HttpStatusCode.BadRequest);
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

            var resultUpdate = PeopleRepository.Update(body, id);

            try
            {
                int result = Convert.ToInt32(resultUpdate);

                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    HttpError err = new HttpError(result.ToString());
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
            }
            catch (Exception)
            {

                HttpError err = new HttpError(resultUpdate);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
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

            var resultDelete = PeopleRepository.Delete(id);

            try
            {
                int result = Convert.ToInt32(resultDelete);

                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    HttpError err = new HttpError(result.ToString());
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
            }
            catch (Exception)
            {

                HttpError err = new HttpError(resultDelete);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }
    }
}
