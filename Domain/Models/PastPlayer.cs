using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PastPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Age { get; set; }
        public string Team { get; set; }
        public float GamesPlayed { get; set; }
        public float MinutesPlayed { get; set; }
        public float PointsPerGame { get; set; }
        public float ReboundsPerGame { get; set; }
        public float AssistsPerGame { get; set; }
        public float StealsPerGame { get; set; }
        public float BlocksPerGame { get; set; }
        public float FieldGoalPercentage { get; set; }
        public float ThreePointPercentage { get; set; }
        public float FreeThrowPercentage { get; set; }
        public int Year { get; set; }
        public string Position { get; set; }

        public static explicit operator PastPlayer(CurrentPlayer v)
        {
            return new PastPlayer{
                Id=v.Id, Name=v.Name, Age=(float)v.Age, Team=v.Team, GamesPlayed=v.GamesPlayed,
                MinutesPlayed=v.MinutesPlayed, PointsPerGame=v.PointsPerGame, ReboundsPerGame=v.ReboundsPerGame,
                AssistsPerGame=v.AssistsPerGame, StealsPerGame=v.StealsPerGame, BlocksPerGame=v.BlocksPerGame,
                FieldGoalPercentage=v.FieldGoalPercentage, ThreePointPercentage=v.ThreePointPercentage, 
                FreeThrowPercentage=v.FreeThrowPercentage, Year=v.Year, Position=v.Position
                };
        }
    }
}