using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{

    public class GrzController : ApiController
    {
        private const string queryGetCards = "select c.id_card, c.id_pep, c.timestart, c.timeend, c.note, c.status," +
            " c.\"ACTIVE\", c.flag, c.id_cardtype from card c where c.id_cardtype=4";


        /// <summary>
        /// Получает список карточек из базы данных с возможностью фильтрации по Гос. номеру автомобиля.
        /// </summary>
        /// <param name="filter">Строка фильтрации по Гос. номеру автомобиля. Параметр являвется не обязательным.
        /// Если не заполнять то фильтр не будет применяться</param>
        /// <returns>Список карточек, соответствующих фильтру.</returns>
        [HttpGet]
        public List<GrzModel> Getlist(string filter = "")
        {
            if (filter.Length > 0)
            {
                return DatabaseService.GetList<GrzModel>(queryGetCards)
                    .Where(x => x.IdCard.Contains(filter)).ToList();
            }

            return DatabaseService.GetList<GrzModel>(queryGetCards);
        }

        /// <summary>
        /// Получить список событий по гос. номеру автомобиля и диапазону дат.
        /// </summary>
        /// <param name="grz">Гос. номер автомобиля.</param>
        /// <param name="dateFrom">Дата "с". Значение по умолчанию: -3 дня от текущей даты.</param>
        /// <param name="dateTo">Дата "по". Значение по умолчанию: текущая дата.</param>
        /// <returns>Список событий.</returns>
        [HttpGet]
        public List<EventModel> EventInfo(
            string grz,
            [SwaggerParameter(Description = "Значение по умолчанию: -3 дня от текущей даты")] DateTime? dateFrom = null,
            [SwaggerParameter(Description = "Значение по умолчанию: текущей даты")] DateTime? dateTo = null)
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
