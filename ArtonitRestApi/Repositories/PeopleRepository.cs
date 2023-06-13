using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;
using System.Security.Claims;

namespace ArtonitRestApi.Repositories
{
    public class PeopleRepository
    {
        public static DatabaseResult GetById(string id)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select ID_PEP, ID_DB, p.ID_ORG, SURNAME, p.NAME, PATRONYMIC,
                DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK,
                NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , p.FLAG,
                LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM
                from people p
                join organization_getchild (1, (select p2. id_orgctrl from people p2 where p2.id_pep={idPep})) og on p.id_org=og.id_org
                where p.ID_PEP = {id}";

            return DatabaseService.Get<People>(query);
        }

        public static DatabaseResult GetByCard(string card, int? cardType = -1)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string query;

            if (cardType == -1)
            {
                query = $@"select p.ID_PEP, p.ID_DB, p.ID_ORG, p.SURNAME, p.NAME, p.PATRONYMIC, p.DATEBIRTH,
                     p.PLACELIFE, p.PLACEREG, p.PHONEHOME, p.PHONECELLULAR, p.PHONEWORK,
                     p.NUMDOC, p.DATEDOC, p.PLACEDOC, p.PHOTO, p.WORKSTART, p.WORKEND, p.""ACTIVE"" , p.FLAG,
                     p.LOGIN, p.PSWD, p.PEPTYPE, p.POST, p.PLACEBIRTH, p.NOTE, p.ID_AREA, p.TABNUM
                     from people p
                     join card c on c.id_pep = p.id_pep
                     join organization_getchild (1, (select p2. id_orgctrl from people p2 where p2.id_pep={idPep})) 
                     og on p.id_org=og.id_org where c.id_card containing '{card}';";
            }
            else
            {
                query = $@"select p.ID_PEP, p.ID_DB, p.ID_ORG, p.SURNAME, p.NAME, p.PATRONYMIC, p.DATEBIRTH,
                     p.PLACELIFE, p.PLACEREG, p.PHONEHOME, p.PHONECELLULAR, p.PHONEWORK,
                     p.NUMDOC, p.DATEDOC, p.PLACEDOC, p.PHOTO, p.WORKSTART, p.WORKEND, p.""ACTIVE"" , p.FLAG,
                     p.LOGIN, p.PSWD, p.PEPTYPE, p.POST, p.PLACEBIRTH, p.NOTE, p.ID_AREA, p.TABNUM
                     from people p
                     join card c on c.id_pep = p.id_pep
                     join organization_getchild (1, (select p2. id_orgctrl from people p2 where p2.id_pep={idPep})) 
                     og on p.id_org=og.id_org where c.id_card containing '{card}' and c.id_cardtype={cardType};";
            }

            return DatabaseService.Get<People>(query);
        }

        public static DatabaseResult GetAll()
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idPep = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = $@"select ID_PEP, ID_DB, ID_ORG, SURNAME, NAME, PATRONYMIC,
                DATEBIRTH, PLACELIFE, PLACEREG, PHONEHOME, PHONECELLULAR, PHONEWORK,
                NUMDOC, DATEDOC, PLACEDOC, PHOTO, WORKSTART, WORKEND, ""ACTIVE"" , FLAG,
                LOGIN, PSWD, PEPTYPE, POST, PLACEBIRTH, NOTE, ID_AREA, TABNUM
                from people p
                where p.id_org in (select id_org from 
                organization_getchild (1, (select p2.id_orgctrl from people p2 where p2.id_pep={idPep})))";

            return DatabaseService.GetList<People>(query);
        }

        public static DatabaseResult Add(PeopleAdd peopleAdd)
        {
            var userIdentity = ClaimsPrincipal.Current;
            var idOrgCtrl = userIdentity?.FindFirst(MyClaimTypes.IdOrgCtrl)?.Value;

            var rdbDatabase = DatabaseService
                .Get<RDBDatabase>("select GEN_ID (gen_people_id, 1) from RDB$DATABASE");

            if(rdbDatabase.State == State.Successes)
            {
                var people = new People();
                people.InitializeFromPeopleAdd(peopleAdd);

                people.Id = Convert.ToInt32(rdbDatabase.Value);
                people.IdDb = 1;
                people.IdArea = 0;
                var defaultDateTime = new DateTime(0);

                if (people.DateBirth == defaultDateTime) people.DateBirth = DateTime.Now;
                if (people.DateDoc == defaultDateTime) people.DateDoc = DateTime.Now;
                if (people.IdOrg == 0) people.IdOrg = Convert.ToInt32(idOrgCtrl);

                var result = DatabaseService.Create(people);

                if(result.State == State.Successes)
                {
                    result.Value = rdbDatabase.Value;
                }

                return result;
            }

            return rdbDatabase;
        }

        public static DatabaseResult Update(PeopleAdd peopleAdd, int id)
        {
            return DatabaseService.Update(peopleAdd, $"ID_PEP={id}");
        }

        public static DatabaseResult Delete(int id)
        {
            var query = $"delete from person where ID_PEP={id}";

            return DatabaseService.ExecuteNonQuery(query);
        }
    }
}
