using ArtonitRestApi.Models;
using ArtonitRestApi.Repositories;
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
            var result = ParkingRepository.GetAll();
            
            if(result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ErrorMessage);
        }

        /// <summary>
        /// Получить список машиномест для указанного гаража
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage ParkingGetById(string id)
        {
            var result = ParkingRepository.GetById(id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ErrorMessage);
        }


        /// <summary>
        /// Добавить гараж на указанную парковку
        /// </summary>
        /// <returns>Результат вставки в формате json</returns>

        [HttpPost]
        public HttpResponseMessage AddParking([FromBody] ParkingModelBase body)
        {
            if (body.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "37 неполные данные");
            }

            var result = ParkingRepository.Add(body);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ErrorMessage);
        }

        /// <summary>
        /// Меняет указанные свойства для указаннома гаража
        /// </summary>
        /// <returns></returns>

        [HttpPatch]
        public HttpResponseMessage UpdateParking(ParkingUpdateDTO body, string id_code)
        {
            var result = ParkingRepository.Update(body, id_code);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ErrorMessage);
        }

        /// <summary>
        /// Удаляет гараж по указанному id
        /// </summary>
        /// <returns>Результат удаления 0 -  успешно, 1 - ошибка</returns>

        [HttpDelete]
        public HttpResponseMessage DelParking(string id)
        {
            var result = ParkingRepository.Delete(id);

            if (result.State == State.Successes)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result.Value);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ErrorMessage);
        }
    }
}
