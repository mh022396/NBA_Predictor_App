using Persistence;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class StatsController : BaseAPIController
    {
        private readonly DataContext context;
        public StatsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{playerName}")]
        public async Task<ActionResult<List<PastPlayer>>> GetPlayerStats(string playerName){
            playerName = playerName.Replace(" ", String.Empty).ToLower();
            List<PastPlayer> playersList = await GetAllStats();
            List<PastPlayer> list = playersList.FindAll(player => player.Name.Replace(" ", String.Empty).ToLower() == playerName);
            if(list.Count < 1) return NotFound();
            return Ok(list);
        }

        [HttpGet("byYear/{year}")]
        public async Task<ActionResult<List<PastPlayer>>> GetStatsByYear(int year){
            if(year == 2022){
                List<CurrentPlayer> l = await context.CurrentPlayers.ToListAsync();
                return Ok( l.Select(player => (PastPlayer)player));
            }
            List<PastPlayer> players = await context.PastPlayers.ToListAsync();
            return Ok(players.FindAll(player => player.Year == year));
        }

        [HttpGet("byTeam")]
        public async Task<ActionResult<List<PastPlayer>>> GetStatsByTeam(string team, int year){
            List<PastPlayer> playersList = await GetAllStats();
            return Ok(playersList.FindAll(player => (player.Team.Replace(" ", String.Empty).ToLower() == team.Replace(" ", String.Empty).ToLower())&& (player.Year == year)));
        }

        private async Task<List<PastPlayer>> GetAllStats(){
            List<PastPlayer> playersList = await context.PastPlayers.ToListAsync();
            foreach(CurrentPlayer player in context.CurrentPlayers){
                playersList.Add((PastPlayer)player);
            }
            return playersList;
        }
    }
}