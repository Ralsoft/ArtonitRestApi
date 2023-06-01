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
    public class SSAccessUserController : ApiController
    {

        [HttpGet]
        public List<SSAccessuserAdd> UserAccessGet() => SSAccessUserRepository.GetAll(); 
        

        [HttpGet]
        public SSAccessuserAdd UserAccessGet(string id) => SSAccessUserRepository.GetById(id);


        [HttpPost]
        public HttpResponseMessage UserAccessAdd([FromBody] SSAccessuserBase body)
        {
          
            var result = SSAccessUserRepository.Add(body);

            try
            {
                var resultInt = Convert.ToInt32(result);
                return Request.CreateResponse(HttpStatusCode.OK, resultInt);
            }
            catch (Exception)
            {
                HttpError err = new HttpError(result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

      
        [HttpDelete]
        public HttpResponseMessage UserAccessDel(int userId, int accessnameId)
        {

            var result = SSAccessUserRepository.Delete(userId, accessnameId);

            try
            {
                var resultInt = Convert.ToInt32(result);
                return Request.CreateResponse(HttpStatusCode.OK, resultInt);
            }
            catch (Exception)
            {
                HttpError err = new HttpError(result);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }
    }
}
