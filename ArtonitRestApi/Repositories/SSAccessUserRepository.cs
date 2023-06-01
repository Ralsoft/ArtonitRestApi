using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtonitRestApi.Repositories
{
    public class SSAccessUserRepository
    {
        public static List<SSAccessuserAdd> GetAll()
        {
            var query = $@"select ID_ACCESSUSER, ID_DB, ID_PEP, ID_ACCESSNAME from ss_accessuser";

            return DatabaseService.GetList<SSAccessuserAdd>(query);
        }

        public static SSAccessuserAdd GetById(string id)
        {
            var query = $@"select ID_ACCESSUSER, ID_DB, ID_PEP, ID_ACCESSNAME from ss_accessuser where ID_ACCESSUSER={id}";

            return DatabaseService.Get<SSAccessuserAdd>(query);
        }

        public static string Add(SSAccessuserBase userBase)
        {

            var userAdd = new SSAccessuserAdd
            {
                IdDb = 1,
            };
            userAdd.InitializeFromSSAccessuserBase(userBase);

            return DatabaseService.Create(userAdd);
        }

        public static string Delete(int userId, int accessnameId)
        {
            var query = $"delete from ss_accessuser ssa where ssa.id_pep = {userId} and ssa.id_accessname={accessnameId}";

            return DatabaseService.ExecuteNonQuery(query);
        }
    }
}
