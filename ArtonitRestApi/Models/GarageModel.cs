using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace ArtonitRestApi.Models
{
    /// <summary>
    /// базовый набор данных о гараже: название и код дивизиона.
    /// </summary>
    [DatabaseName("HL_GARAGENAME")]
    public class GarageModel
    {
        [DatabaseName("ID")]
        public int Id { get; set; }

        [DatabaseName("NAME")]
        public string Name { get; set; }

        [DatabaseName("DIV_CODE")]
        public string DivCode { get; set; }

        [DatabaseName("CREATED")]
        public DateTime DateStart { get; set; }

        [DatabaseName("NOT_COUNT")]
        public int NotCount { get; set; }
    }
}
