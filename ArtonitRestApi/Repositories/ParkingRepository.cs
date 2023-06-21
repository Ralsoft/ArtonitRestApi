using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;

namespace ArtonitRestApi.Repositories
{
    public class ParkingRepository
    {
        public static DatabaseResult GetAll()
        {
            /*
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
                where p.parent=14";
            */
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p";


            return DatabaseService.GetList<ParkingModel>(query);
        }

        public static DatabaseResult GetById(string id)
        {
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
            where p.parent=14 and p.id = {id}";
            return DatabaseService.Get<ParkingModel>(query);
        }

        public static DatabaseResult Add(ParkingModelBase parkingModelBase)
        {
            var rdbDatabase = DatabaseService
               .Get<RDBDatabase>("select GEN_ID (gen_hl_parking_id, 1) from RDB$DATABASE");


            if (rdbDatabase.State == State.Successes)
            {
                var parkingModel = new ParkingModel();
                parkingModel.Id = (rdbDatabase.Value as RDBDatabase).Id;
                parkingModel.Init(parkingModelBase);
                parkingModel.Parent = 14;

                var result = DatabaseService.Create(parkingModel);

                if(result.State == State.Successes)
                {
                    result.Value = parkingModel.Id;
                }
               
                return result;
            }

            return rdbDatabase;
        }

        public static DatabaseResult Update(ParkingUpdateDTO parkingModelBase, string id)
        {
            return DatabaseService.Update(parkingModelBase, $"ID={id}");
        }

        public static DatabaseResult Delete(string id)
        {
            var query = $"delete from HL_PARKING where ID={id}";
            return DatabaseService.ExecuteNonQuery(query);
        }
    }
}
