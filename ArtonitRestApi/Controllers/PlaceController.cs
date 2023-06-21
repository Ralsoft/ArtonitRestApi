using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
using ArtonitRestApi.Services;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetPlaceList()
        {

            var result = PlaceRepository.GetAll();

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,
                new HttpError(result.ErrorMessage));
        }

      

        /// <summary>
        /// Добавить машиноместо на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]
        public HttpResponseMessage AddPlace([FromBody] PlaceModel body)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Удаляет машиноместо по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>
        [HttpDelete]
        public HttpResponseMessage DelPlaceL()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        /// <summary>
        /// Меняет указанные свойства для указаннома машиноместа
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage UpdatePlaceL()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }


        /// <summary>
        /// Получить свойств парковочного места
        /// </summary>
        /// <returns>Результат</returns>
        
        [HttpGet]
        public HttpResponseMessage GetPlaceById(int id)
        {
            var query = $@"select * from hl_place hlp where hlp.id={id}";

            var result = DatabaseService.Get<PlaceModel>(query);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new HttpError(result.ErrorMessage));
            }
        }
    }
}
