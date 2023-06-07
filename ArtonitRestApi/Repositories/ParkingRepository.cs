using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtonitRestApi.Repositories
{
    public class ParkingRepository
    {
        public static List<ParkingModel> GetAll()
        {
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
            where p.parent=14";
            return DatabaseService.GetList<ParkingModel>(query);
        }


        //реализация через NonQuery 7.06.2023
        public static List<ParkingModel> GetAll_nq()//
        {
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
            where p.parent=14";
            /*
             что и как тут надо сделать, чтобы собрать ответ и при этом испльзовать только ExecuteNonQuery(string query) и НЕ использовать GetList<T>(string query)? 
             */
            return DatabaseService.GetList<ParkingModel>(query);
        }



        public static string Add(ParkingModelBase parkingModelBase)
        {
            
            var parkingModel = new ParkingModel();
            parkingModel.Init(parkingModelBase);
            parkingModel.Parent = 14;
            parkingModel.Parent = 14;
            

            var query = DatabaseService.GenerateCreateQuery(parkingModel);
            var result = DatabaseService.ExecuteNonQuery(query);
            return result.ToString();
        }

        public static string Update(ParkingModel parkingModelBase, string id_code)
        {
            var result = DatabaseService.Update(parkingModelBase, $"ID_CODE={id_code}");
            return result.ToString();
        }
    }
}
