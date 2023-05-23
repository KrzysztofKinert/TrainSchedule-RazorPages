using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainSchedule.Models;

namespace TrainSchedule.Data
{
    public class DbInitializer
    {
        public static void Initialize(TrainSchedule_db context)
        {
            if (context.Connections.Any())
            {
                return;
            }

            var connections = new Connection[]
            {
                new Connection{Name="A",Type="B",IsWiFi=true,IsBicycleCarriage=false}
            };

            context.Connections.AddRange(connections);
            context.SaveChanges();

            var stages = new Stage[]
            {
                new Stage{ConnectionID=1,Sequence=1,DepartureStation="Opole",ArrivalStation="Brzeg",DepartureDate=DateTime.Now,ArrivalDate=DateTime.Now,Distance=40.0},
                new Stage{ConnectionID=1,Sequence=2,DepartureStation="Brzeg",ArrivalStation="Wroc",DepartureDate=DateTime.Now,ArrivalDate=DateTime.Now,Distance=42.0}
            };

            context.Stages.AddRange(stages);
            context.SaveChanges();

            var stations = new Station[]
            {
                new Station{Name="Opole Główne"},
                new Station{Name="Opole Zachodnie"},
                new Station{Name="Chróścina Opolska"},
                new Station{Name="Dąbrowa Niemodlinska"},
                new Station{Name="Lewin Brzeski"},
                new Station{Name="Przecza"},
                new Station{Name="Brzeg"},
                new Station{Name="Lipki"},
                new Station{Name="Oława"},
                new Station{Name="Lizawice"},
                new Station{Name="Zębice Wrocławskie"},
                new Station{Name="Święta Katarzyna"},
                new Station{Name="Wrocław Brochów"},
                new Station{Name="Wrocław Główny"}
            };

            context.Stations.AddRange(stations);
            context.SaveChanges();

        }

        
    }
}
