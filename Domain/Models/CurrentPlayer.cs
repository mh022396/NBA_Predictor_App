using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CurrentPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Team { get; set; }
        public int GamesPlayed { get; set; }
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
    }
}