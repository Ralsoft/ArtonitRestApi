using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetParkingList()
        {
            var list = ParkingRepository.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }


        /// <summary>
        /// Добавить гараж на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]
        public HttpResponseMessage AddParking([FromBody] ParkingModelBase body)
        {
            string result = "";
            if (body.Id_div == null || body.Name == null)
            {
                result = "37 неполные данные";
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = ParkingRepository.Add(body);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// Удаляет гараж по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>

        [HttpDelete]
        public HttpResponseMessage DelParking()
        { 
            //DatabaseService.GetList<ParkingModel>("select * from hl_parking");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Меняет указанные свойства для указаннома гаража
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage UpdateParking(ParkingModel body, string id_code)
        {

            string result = "";
            /*
            if (body.Id_div == null || body.Name == null )
            {
                result = "37 неполные данные";
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            */

            result = ParkingRepository.Update(body, id_code);
            return Request.CreateResponse(HttpStatusCode.OK, result);
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
