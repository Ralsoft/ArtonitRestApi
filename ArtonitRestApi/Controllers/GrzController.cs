using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using ArtonitRestApi.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{

    public class GrzController : ApiController
    {

        /// <summary>
        /// Получает список карточек из базы данных с возможностью фильтрации по Гос. номеру автомобиля.
        /// </summary>
        /// <param name="filter">Строка фильтрации по Гос. номеру автомобиля. Параметр являвется не обязательным.
        /// Если не заполнять то фильтр не будет применяться</param>
        /// <returns>Список карточек, соответствующих фильтру.</returns>
        
        [HttpGet]
        public HttpResponseMessage Getlist(string filter = "")
        {
            var result = GrzRepository.GetAll(filter);
            return Request.CreateResponse(HttpStatusCode.OK, result);
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
            return GrzRepository.EventInfo(grz, dateFrom, dateTo);
        }
    }
}
