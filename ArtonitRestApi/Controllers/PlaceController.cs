using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class PlaceController : ApiController
    {

        /// <summary>
        /// Получает список мест из базы данных по запросу select * from hl_place
        /// </summary>
        /// <returns>Список мест в формате json</returns>
        [HttpGet]
        public List<PlaceModel> GetList() => DatabaseService.GetList<PlaceModel>("select * from hl_place");
    }
}
