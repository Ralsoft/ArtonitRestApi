using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class GarageController : ApiController
    {

        /// <summary>
        /// Получает список гаражей
        /// </summary>
        /// <returns>Список мест в формате json</returns>

        [HttpGet]
        public HttpResponseMessage GetGarageList()
        {
            var result = GarageRepository.GetList();

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        /// <summary>
        /// Добавить гараж.
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]
        public HttpResponseMessage AddGarage([FromBody] GarageModel body)
        {
            var result = GarageRepository.Add(body);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }


        /// <summary>
        /// Удаляет гараж по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>
        
        [HttpDelete]
        public HttpResponseMessage DelGarage()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Меняет указанные свойства для указаннома гаража
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage UpdateGarage()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Получить список машиномест для указанного гаража
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet]
        public HttpResponseMessage GarageGetById(string id)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
    }
}
