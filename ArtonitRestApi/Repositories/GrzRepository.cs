using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtonitRestApi.Repositories
{
    public class GrzRepository
    {
        public static List<GrzModel> GetAll(string filter)
        {
            var queryGetCards = "select c.id_card, c.id_pep, c.timestart, c.timeend, c.note, c.status, " +
            "c.\"ACTIVE\", c.flag, c.id_cardtype from card c where c.id_cardtype=4";

            if (filter.Length > 0)
            {
                return DatabaseService.GetList<GrzModel>(queryGetCards)
                    .Where(x => x.IdCard.Contains(filter)).ToList();
            }

            return DatabaseService.GetList<GrzModel>(queryGetCards);
        }

        public static List<EventModel> EventInfo(string grz, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (dateFrom == null) dateFrom = DateTime.Now.AddDays(-3);
            if (dateTo == null) dateTo = DateTime.Now;

            string query = "select e.event_code, e.event_time, e.created, hlp.is_enter, hlp.id_parking from hl_events e " +
                "join hl_param hlp on hlp.id_dev=e.id_gate " +
                $"where e.grz='{grz}' " +
                "and e.event_code in (46, 50, 65, 81) " +
                $"and e.event_time>'{dateFrom}' " +
                $"and e.event_time<'{dateTo}' " +
                "order by e.event_time";

            return DatabaseService.GetList<EventModel>(query);
        }
    }
}
