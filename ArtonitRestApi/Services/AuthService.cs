using ArtonitRestApi.Models;
using System;
using System.Security.Claims;

namespace ArtonitRestApi.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            var query = $@"select p.id_pep, p.flag, p.id_orgctrl from people p 
                where p.login='{username}' and p.pswd='{password}' and p.""ACTIVE"">0";

            var result = DatabaseService.Get<User>(query);

            return result.Value as User;
        }

        public static bool CheckRightUserAdd()
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idOrgCtrl = userIdentity?.FindFirst(MyClaimTypes.IdOrgCtrl)?.Value;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select og.id_org from organization_getchild 
                (1, (select p. id_orgctrl from people p where p.id_pep={idPep})) 
                og where og.id_org={idOrgCtrl}";

            var result = DatabaseService.Get<RightsVerificationModel>(query);

            if(result.State == State.Successes)
            {
                return Convert.ToInt32((result.Value as RightsVerificationModel).Id) != 0;
            }

            return false;
        }

        public static bool CheckRightUserUpdateDelete(int id)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var queryVerify = $@"select p2.id_pep from people p2 
                join organization_getchild (1, (select p. id_orgctrl from people p where p.id_pep={idPep})) 
                og on p2.id_org=og.id_org where p2.id_pep={id}";

            var result = DatabaseService.Get<RightsVerificationModel>(queryVerify);

            if (result.State == State.Successes)
            {
                return Convert.ToInt32((result.Value as RightsVerificationModel).Id) != 0;
            }

            return false;
        }

        public static bool CheckRightCardAdd()
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idOrgCtrl = userIdentity?.FindFirst(MyClaimTypes.IdOrgCtrl)?.Value;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var queryVerify = $@"select p.id_pep from people p 
                 join organization_getchild (1, {idOrgCtrl}) og on og.id_org=p.id_org
                 where p.id_pep={idPep}";

            var result = DatabaseService.Get<RightsVerificationModel>(queryVerify);

            if (result.State == State.Successes)
            {
                return Convert.ToInt32((result.Value as RightsVerificationModel).Id) != 0;
            }

            return false;
        }
    }
}
