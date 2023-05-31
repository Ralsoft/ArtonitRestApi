using ArtonitRestApi.Models;
using ArtonitRestApi.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtonitRestApi.Repositories
{
    public class GarageRepository
    {
        public static List<GarageModel> GetList()
        {
         
            var query = "select hlgn.id, hlgn.name, hlgn.created, hlgn.not_count, hlgn.div_code from HL_GARAGENAME hlgn";

            return DatabaseService.GetList<GarageModel>(query);
            

        }

        public static int Add(GarageModelBase garageModelBase)
        {
            var rdbDatabase = DatabaseService
                .Get<RDBDatabase>("select GEN_ID (GEN_HL_GARAGENAME_ID, 1) from RDB$DATABASE");

            var garage = new GarageModelAdd();

            garage.InitializeFromGarageModelBase(garageModelBase);

            garage.Id = rdbDatabase.Id;
           
            var result = DatabaseService.Create(garage);

            if (result == "ok")
            {
                return rdbDatabase.Id;
            }

            return 0;
        }


    }
}
