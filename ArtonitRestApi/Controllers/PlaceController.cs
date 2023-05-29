using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
        //public List<PlaceModel> GetPlaceList() => DatabaseService.GetList<PlaceModel>("select * from hl_place");
        public List<PlaceModel> getPlaceList()
        {

             var query = $@"select hlp.id, hlp.placenumber, hlp.description, hlp.note, hlp.status, hlp.name, hlp.id_parking from hl_place hlp";
            return DatabaseService.GetList<PlaceModel>(query);
        }
        
        /// <summary>
        /// Добавить машиноместо на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>
        
        [HttpPost]
        //public List<PlaceModel> addPlaceL() => DatabaseService.Create<PlaceModel>("select * from hl_place");
        public HttpResponseMessage addPlace([FromBody] PlaceModel body)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// Удаляет машиноместо по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>
        [HttpDelete]
        public HttpResponseMessage delPlaceL()
        {
            //DatabaseService.GetList<PlaceModel>("select * from hl_place");
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Меняет указанные свойства для указаннома машиноместа
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage updatePlaceL()
        {
            DatabaseService.GetList<PlaceModel>("select * from hl_place");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Получить свойств парковочного места
        /// </summary>
        /// <returns>Результат</returns>

       
        [HttpGet]

        public HttpResponseMessage getPlaceById(int id)
        {
            
            //var query = $@"select * from hl_place hlp where hlp.id={id}";

            //return DatabaseService.Get<PlaceModel>(query);
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }









    }



}
