using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace ArtonitRestApi.Models
{
    /// <summary>
    /// базовый набор данных о гараже: название и код дивизиона.
    /// </summary>
    [DatabaseName("HL_GARAGENAME")]
    public class GarageModelBase
    {
        [DatabaseName("NAME")]
        public string Name { get; set; }

        [DatabaseName("DIV_CODE")]
        public string DivCode { get; set; }
    }


    /// <summary>
    /// набор данных для регистрации. При вставке данных значение ID берется из генератора, и это же значение передается в return после выполнения команды добавления гаража.
    /// </summary>

    [DatabaseName("HL_GARAGENAME")]
    public class GarageModelAdd : GarageModelBase
    {
        [DatabaseName("ID")]
        public int? Id { get; set; }
        public void InitializeFromGarageModelBase(GarageModelBase garageModelBase)
        {
            Name = garageModelBase.Name;
            DivCode = garageModelBase.DivCode;
        }

    }

   /// <summary>
   /// полный набор данных о гараже.
   /// </summary>
    public class GarageModel : GarageModelBase
    {
        [DatabaseName("ID")]
        public int Id { get; set; }

        [DatabaseName("CREATED")]
        public DateTime DateStart { get; set; }

        [DatabaseName("NOT_COUNT")]
        public int NotCount { get; set; }

        
        public void InitializeFromGarageModelBase(GarageModelBase garageModelBase)
        {
            Name = garageModelBase.Name;
            DivCode = garageModelBase.DivCode;
        }
    }
}
