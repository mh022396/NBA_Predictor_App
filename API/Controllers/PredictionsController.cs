using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PredictionsController : BaseAPIController
    {
        private readonly DataContext context;
        public PredictionsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MVPPrediction>>> GetPredictions(){
            return await context.Predictions.ToListAsync();
        }

        [HttpGet("{rank}")]
        public async Task<ActionResult<MVPPrediction>> GetPredictions(int rank){
            return await context.Predictions.FirstAsync(pred => pred.PredictedRank == rank);
        }
    }
}