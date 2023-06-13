using ArtonitRestApi.Models;
using ArtonitRestApi.Services;

namespace ArtonitRestApi.Repositories
{
    public class ParkingRepository
    {
        public static DatabaseResult GetAll()
        {
            var query = $@"select p.id, p.name, p.enabled, p.created, p.parent from hl_parking p
                where p.parent=14";

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
            var parkingModel = new ParkingModel();
            parkingModel.Init(parkingModelBase);
            parkingModel.Parent = 14;

            var query = DatabaseService.GenerateCreateQuery(parkingModel);
            return DatabaseService.ExecuteNonQuery(query);
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
