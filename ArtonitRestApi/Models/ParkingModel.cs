using ArtonitRestApi.Annotation;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Web.UI.WebControls.WebParts;

namespace ArtonitRestApi.Models
{
    [DatabaseName("HL_PARKING")]
    public class ParkingModelBase
    {

        [DatabaseName("NAME")]
        public string Name { get; set; }  


       // [DatabaseName("ID_DIV")]
       // public string Id_div { get; set; }

    }

    [DatabaseName("HL_PARKING")]
    public class ParkingModel : ParkingModelBase
    {
        [DatabaseName("ID")]
        [DatabasePrimaryKey]
        public int Id { get; set; }

       [DatabaseName("parent")]
        public int? Parent { get; set; }

        public void Init(ParkingModelBase parkingModel)
        {
            Name = parkingModel.Name;
            //Id_div = parkingModel.Id_div;
        }
    }

   
}
