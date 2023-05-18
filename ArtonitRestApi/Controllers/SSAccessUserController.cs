﻿using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Collections.Generic;
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
        public string UserAccessAdd([FromBody] Ss_accessuser body)
        {
            body.IdDb = 1;

            return DatabaseService.Create(body);


        }
      
        [HttpDelete]
        public string UserAccessDel(int userId, int accessnameId)
        {
            var query = $"delete from ss_accessuser ssa where ssa.id_pep ={userId} and ssa.id_accessname={accessnameId}";

            return DatabaseService.ExecuteNonQuery(query);
        }
    }
}
