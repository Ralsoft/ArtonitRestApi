using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;

namespace ArtonitRestApi.Repositories
{
    public class GarageRepository
    {
        public static DatabaseResult GetList()
        {
            var query = @"select hlgn.id, hlgn.name, hlgn.created, hlgn.not_count, 
                            hlgn.div_code from HL_GARAGENAME hlgn";

            return DatabaseService.GetList<GarageModel>(query);
        }

        public static DatabaseResult Add(GarageModel garageModelBase)
        {
            var rdbDatabase = DatabaseService
                .Get<RDBDatabase>("select GEN_ID (GEN_HL_GARAGENAME_ID, 1) from RDB$DATABASE");

            var garage = new GarageModel();

            if(rdbDatabase.State == State.Successes)
            {
                garage.Id = Convert.ToInt32(rdbDatabase.Value);

                var result = DatabaseService.Create(garage);

                if (result.State == State.Successes)
                    result.Value = 0;

                return result;
            }

            return rdbDatabase;
        }

        public static DatabaseResult GetById(string id)
        {
            var query = $@"select hlgn.id, hlgn.name, hlgn.created, hlgn.not_count, 
                            hlgn.div_code from HL_GARAGENAME hlgn
                            where hlgn.id={id}";
            return DatabaseService.Get<GarageModel>(query);
        }


    }
}
