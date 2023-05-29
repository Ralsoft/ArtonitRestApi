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
    public class ParkingController : ApiController
    {

        /// <summary>
        /// Получает список парковок из базы данных 
        /// </summary>
        /// <returns>Список мест в формате json</returns>

        [HttpGet]
        public List<ParkingModel> GetParkingList()
        {
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
            where p.parent=14";
            return DatabaseService.GetList<ParkingModel>(query);

        }



        /// <summary>
        /// Добавить гараж на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]

        public HttpResponseMessage addParking([FromBody] ParkingModel body)
        {
           // return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.NotImplemented);

        }

        /// <summary>
        /// Удаляет гараж по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>

        [HttpDelete]
        public HttpResponseMessage delParking()
        { 
            //DatabaseService.GetList<ParkingModel>("select * from hl_parking");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Меняет указанные свойства для указаннома гаража
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage updateParking()
        {
            //DatabaseService.GetList<ParkingModel>("select * from HL_ParkingNAME");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

            /// <summary>
            /// Получить список машиномест для указанного гаража
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>

          [HttpGet]
        public HttpResponseMessage ParkingGetById(string id)
        {
          var query = $@"select * from hl_Parking hlg
                where hlg.id_Parkingname= {id}";
           // return DatabaseService.Get<ParkingModel>(query);
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }




    }



}
