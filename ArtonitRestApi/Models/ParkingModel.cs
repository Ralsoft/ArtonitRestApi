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
    }

    public class ParkingUpdateDTO : ParkingModelBase
    {
        [DatabaseName("parent")]
        public int? Parent { get; set; }

        public void Init(ParkingModelBase parkingModel)
        {
            Name = parkingModel.Name;
        }
    }

    [DatabaseName("HL_PARKING")]
    public class ParkingModel : ParkingUpdateDTO
    {
        [DatabaseName("ID")]
        public int Id { get; set; }

        public void Init(ParkingUpdateDTO parkingModel)
        {
            Name = parkingModel.Name;
            Parent = parkingModel.Parent;
        }
    }  
}
