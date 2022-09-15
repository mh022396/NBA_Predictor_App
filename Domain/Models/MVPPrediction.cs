using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MVPPrediction
    {
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal PredictedShare { get; set; } 
        public virtual int PredictedRank { get; set; }
    }
}