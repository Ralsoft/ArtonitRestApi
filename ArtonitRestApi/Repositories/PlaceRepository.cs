using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;

namespace ArtonitRestApi.Repositories
{
    public class PlaceRepository
    {
        public static DatabaseResult GetAll()
        {
  

            // var query = $@"select hlp.id, hlp.placenumber, hlp.description, hlp.note, hlp.status, hlp.name, hlp.id_parking from hl_place hlp";
             var query = $@"SELECT HLP.ID, COALESCE(HLP.PLACENUMBER, '') as PLACENUMBER, 
                        COALESCE(HLP.DESCRIPTION, '') AS DESCRIPTION, 
                        COALESCE(HLP.NOTE, '') AS NOTE, 
                        COALESCE(HLP.STATUS, '') AS STATUS, 
                        COALESCE(HLP.NAME,'') AS NAME,  
                        COALESCE(HLP.ID_PARKING, '') AS ID_PARKING 
                        FROM HL_PLACE HLP";

            return DatabaseService.GetList<PlaceModel>(query);
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
