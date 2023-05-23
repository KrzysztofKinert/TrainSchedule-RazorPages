using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainSchedule.Models;

namespace TrainSchedule.Data
{
    public static class SelectResults
    {
        public static async Task<List<Result>> FindResults(TrainSchedule_db context, Condition conditions)
        {
            var results = new List<Result>();

            var select = from stages in context.Stages
                         where stages.DepartureStation == conditions.FromStation
                         select stages.ConnectionID;
            List<int> IdsFrom = await select.ToListAsync();

            select = from stages in context.Stages
                     where stages.ArrivalStation == conditions.ToStation
                     select stages.ConnectionID;
            List<int> IdsTo = await select.ToListAsync();

            DateTime dateStart = conditions.Date;
            dateStart = dateStart.AddHours(conditions.Time.Hour - 1);
            dateStart = dateStart.AddMinutes(conditions.Time.Minute);
            DateTime dateEnd = dateStart.AddHours(11);

            if (conditions.IsDeparture == true)
            {
                select = from stages in context.Stages
                              where stages.DepartureDate >= dateStart
                                 && stages.DepartureDate <= dateEnd
                                 && stages.DepartureStation == conditions.FromStation
                              select stages.ConnectionID;
            }
            else
            {
                select = from stages in context.Stages
                              where stages.ArrivalDate >= dateStart
                                 && stages.ArrivalDate <= dateEnd
                                 && stages.ArrivalStation == conditions.ToStation
                              select stages.ConnectionID;
            }
            List<int> IdsDate = await select.ToListAsync();

            List<int> IdsType = new List<int>();
            if(conditions.IsExpress == true)
            {
                select = from connections in context.Connections
                         where connections.Type == "E"
                         select connections.ConnectionID;
                IdsType.AddRange(await select.ToListAsync());
            }
            if (conditions.IsIntercity == true)
            {
                select = from connections in context.Connections
                         where connections.Type == "I"
                         select connections.ConnectionID;
                IdsType.AddRange(await select.ToListAsync());
            }
            if (conditions.IsRegional == true)
            {
                select = from connections in context.Connections
                         where connections.Type == "R"
                         select connections.ConnectionID;
                IdsType.AddRange(await select.ToListAsync());
            }

            List<int> IdsWifi = new List<int>();
            if (conditions.IsWifi == true)
            {
                select = from connections in context.Connections
                         where connections.IsWiFi == true
                         select connections.ConnectionID;
                IdsWifi.AddRange(await select.ToListAsync());
            }
            else
            {
                select = from connections in context.Connections
                         select connections.ConnectionID;
                IdsWifi.AddRange(await select.ToListAsync());
            }

            List<int> IdsBicycleCarriage = new List<int>();
            if (conditions.IsBicycleCarriage == true)
            {
                select = from connections in context.Connections
                         where connections.IsBicycleCarriage == true
                         select connections.ConnectionID;
                IdsBicycleCarriage.AddRange(await select.ToListAsync());
            }
            else
            {
                select = from connections in context.Connections
                         select connections.ConnectionID;
                IdsBicycleCarriage.AddRange(await select.ToListAsync());
            }

            HashSet<int> intersection = new HashSet<int>(IdsFrom);
            intersection.IntersectWith(IdsTo);
            intersection.IntersectWith(IdsDate);
            intersection.IntersectWith(IdsType);
            intersection.IntersectWith(IdsWifi);
            intersection.IntersectWith(IdsBicycleCarriage);
            List<int> Ids = intersection.ToList();

            foreach (int id in Ids) results.Add(await SelectResult(context, conditions, id));

            List<Result> sortedResults = new List<Result>();
            if(conditions.IsDeparture == true) sortedResults = results.OrderBy(r => r.Departure).ToList();
            else sortedResults = results.OrderBy(r => r.Arrival).ToList();

            return sortedResults;
        }
        public static async Task<Result> SelectResult(TrainSchedule_db context, Condition conditions, int id)
        {
            Result result = new Result();

            var selectString = from connections in context.Connections
                               where connections.ConnectionID == id
                               select connections.Name;
            result.Name = await selectString.FirstOrDefaultAsync();

            result.ConnectionID = id;

            var selectDate = from stages in context.Stages
                             where stages.ConnectionID == id
                              && stages.DepartureStation == conditions.FromStation
                             select stages.DepartureDate;
            result.Departure = await selectDate.FirstOrDefaultAsync();

            selectDate = from stages in context.Stages
                         where stages.ConnectionID == id
                            && stages.ArrivalStation == conditions.ToStation
                         select stages.ArrivalDate;
            result.Arrival = await selectDate.FirstOrDefaultAsync();

            result.Stages = await SelectStages(context, conditions, id);

            result.Distance = 0;
            foreach (Stage stage in result.Stages) result.Distance += stage.Distance;

            var selectService = from connections in context.Connections
                                where connections.ConnectionID == id
                                select connections.IsBicycleCarriage;
            result.IsBicycleCarriage = await selectService.FirstOrDefaultAsync();

            selectService = from connections in context.Connections
                            where connections.ConnectionID == id
                            select connections.IsWiFi;
            result.IsWiFi = await selectService.FirstOrDefaultAsync();

            var selectType = from connections in context.Connections
                             where connections.ConnectionID == id
                             select connections.Type;
            result.Type = await selectType.FirstOrDefaultAsync();

            result.Time = result.Arrival.Subtract(result.Departure);

            return result;
        }

        private static async Task<List<Stage>> SelectStages(TrainSchedule_db context, Condition conditions, int ConnectionID)
        {
            var selectSequence = from stages in context.Stages
                                 where stages.DepartureStation == conditions.FromStation
                                    && stages.ConnectionID == ConnectionID
                                 select stages.Sequence;
            int minStage = await selectSequence.FirstAsync();

            selectSequence = from stages in context.Stages
                             where stages.ArrivalStation == conditions.ToStation
                                && stages.ConnectionID == ConnectionID
                             select stages.Sequence;
            int maxStage = await selectSequence.FirstAsync();

            var selectStages = from stages in context.Stages
                               where stages.Sequence >= minStage
                                && stages.Sequence <= maxStage
                                && stages.ConnectionID == ConnectionID
                               select stages;

            return await selectStages.ToListAsync();
        }
    }
}
