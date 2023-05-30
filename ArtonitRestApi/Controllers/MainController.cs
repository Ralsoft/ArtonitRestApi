using ArtonitRestApi.Services;
using System;
using System.Web.Http;

namespace ArtonitRestApi.Controllers
{
    public class MainController : ApiController
    {

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
