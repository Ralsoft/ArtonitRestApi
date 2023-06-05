using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.Json;

namespace ArtonitRestApi.Controllers
{
    public class MainController : ApiController
    {

        /// <summary>
        /// Тест Бухаров 3.06.2023
        /// </summary>
        /// <returns>Изучение....</returns>

        [HttpGet]
        public HttpResponseMessage Test1()
        {
            //var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p where p.parent=14";
            var placeModel = new PlaceModel
            {
                Id = 37,
                PlaceNumber = 22,
                Description="test_15",
                Note="Note",
                Status="Status",
                Name="Name",
                IdParking=4, 

            };
        
            string jsonString = JsonSerializer.Serialize(placeModel);
            //LoggerService.Log<DatabaseService>("Info", jsonString);
            LoggerService.Log<MainController>("Info", jsonString);

            return Request.CreateResponse(HttpStatusCode.OK, jsonString);
        }


       /// <summary>
        /// Возвращает настройки программы
        /// </summary>
        /// <returns>Текущие настройки программы в виде json.</returns>
        
        [HttpGet]
        public SettingsService Info() => new SettingsService();
      
        
        
        /// <summary>
        /// Возвращает текущее время
        /// </summary>
        /// <returns>Текущие время в UTC.</returns>
        
        [HttpGet]
        public DateTime ServerTime() => DateTime.UtcNow;


        /// <summary>
        /// Возвращает строку переданную в параметр, если ничего не переданно возвращает default
        /// </summary>
        /// <param name="key">строка, которая будет возвращена</param>
        /// <returns>параметр key</returns>
        
        [HttpGet]
        public string GetString(string key = "default") => key; 
    }
}
