using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Domain.Models;

namespace Persistence.ModelMappers
{
    public class ModelDataMapper
    {
        public class MVPPredicitionClassMap : ClassMap<MVPPrediction>
        {
            public MVPPredicitionClassMap()
            {
                Map(m => m.Name).Name("Player");
                Map(m => m.PredictedRank).Name("Predicted_Rk");
                Map(m => m.PredictedShare).Name("predictions");
            }
        }

        public class PastMVPClassMap : ClassMap<PastMVP>
        {
            public PastMVPClassMap()
            {
                Map(m => m.Name).Name("Player").Default("Unknown");
                Map(m => m.Age).Name("Age").Default(null);
                Map(m => m.Team).Name("Tm").Default("Unknown");
                Map(m => m.GamesPlayed).Name("G").Default(0);
                Map(m => m.MinutesPlayed).Name("MP").Default(0);
                Map(m => m.PointsPerGame).Name("PTS").Default(0);
                Map(m => m.ReboundsPerGame).Name("TRB").Default(0);
                Map(m => m.AssistsPerGame).Name("AST").Default(0);
                Map(m => m.StealsPerGame).Name("STL").Default(0);
                Map(m => m.BlocksPerGame).Name("BLK").Default(0);
                Map(m => m.FieldGoalPercentage).Name("FG%").Default(0);
                Map(m => m.ThreePointPercentage).Name("3P%").Default(0);
                Map(m => m.FreeThrowPercentage).Name("FT%").Default(0);
                Map(m => m.Year).Name("Year");
                Map(m => m.Rank).Name("Rank");
                Map(m => m.MVPShare).Name("Share");
            }
        }

        public class CurrentPlayerClassMap : ClassMap<CurrentPlayer>
        {
            public CurrentPlayerClassMap()
            {
                Map(m => m.Name).Name("Player").Default("Unknown");
                Map(m => m.Age).Name("Age").Default(null);
                Map(m => m.Team).Name("Tm").Default("Unknown");
                Map(m => m.GamesPlayed).Name("G").Default(0);
                Map(m => m.MinutesPlayed).Name("MP").Default(0);
                Map(m => m.PointsPerGame).Name("PTS").Default(0);
                Map(m => m.ReboundsPerGame).Name("TRB").Default(0);
                Map(m => m.AssistsPerGame).Name("AST").Default(0);
                Map(m => m.StealsPerGame).Name("STL").Default(0);
                Map(m => m.BlocksPerGame).Name("BLK").Default(0);
                Map(m => m.FieldGoalPercentage).Name("FG%").Default(0);
                Map(m => m.ThreePointPercentage).Name("3P%").Default(0);
                Map(m => m.FreeThrowPercentage).Name("FT%").Default(0);
                Map(m => m.Year).Name("Year");
                Map(m => m.Position).Name("Pos").Default("Unknown");
            }
        }

        public class PastPlayerClassMap : ClassMap<PastPlayer>
        {
            public PastPlayerClassMap()
            {
                Map(m => m.Name).Name("Player").Default("Unknown");
                Map(m => m.Age).Name("Age").Default(null);
                Map(m => m.Team).Name("Tm").Default("Unknown");
                Map(m => m.GamesPlayed).Name("G").Default(0);
                Map(m => m.MinutesPlayed).Name("MP").Default(0);
                Map(m => m.PointsPerGame).Name("PTS").Default(0);
                Map(m => m.ReboundsPerGame).Name("TRB").Default(0);
                Map(m => m.AssistsPerGame).Name("AST").Default(0);
                Map(m => m.StealsPerGame).Name("STL").Default(0);
                Map(m => m.BlocksPerGame).Name("BLK").Default(0);
                Map(m => m.FieldGoalPercentage).Name("FG%").Default(0);
                Map(m => m.ThreePointPercentage).Name("3P%").Default(0);
                Map(m => m.FreeThrowPercentage).Name("FT%").Default(0);
                Map(m => m.Year).Name("Year");
                Map(m => m.Position).Name("Pos").Default("Unknown");
            }
        }
    }
}