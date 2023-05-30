using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class GarageController : ApiController
    {

        /// <summary>
        /// Получает список гаражей из базы данных по запросу select * from HL_GARAGENAME
        /// </summary>
        /// <returns>Список мест в формате json</returns>

        [HttpGet]
        public List<GarageModel> GetGarageList()
        {
          var query = $@"select hlgn.id, hlgn.name, hlgn.created, hlgn.NotCount from HL_GARAGENAME hlgn";
            return DatabaseService.GetList<GarageModel>(query);
           
        }


        /// <summary>
        /// Добавить гараж на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]
        public HttpResponseMessage AddGarage([FromBody] GarageModel body)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Удаляет гараж по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>
        
        [HttpDelete]
        public HttpResponseMessage DelGarage()
        {
            // DatabaseService.GetList<GarageModel>("select * from HL_GARAGENAME");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Меняет указанные свойства для указаннома гаража
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage UpdateGarage()
        {
            //DatabaseService.GetList<GarageModel>("select * from HL_GARAGENAME");
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Получить список машиномест для указанного гаража
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet]
        public GarageModel GarageGetById(string id)
        {
          var query = $@"select hlgn.id, hlgn.name, hlgn.created, hlgn.NotCount from HL_GARAGENAME hlgn
                where hlgn.id= {id}";
            return DatabaseService.Get<GarageModel>(query);
        }
    }
}
