#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FIGProject.DAL;
using FIGProject.Models;

namespace FIGProject.Controllers
{
    [Route("api/[controller]/[action]")] 
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsOrderedByName()
        {
            var teams = await _context.Teams.ToListAsync();
            var teamsOrderedByName = teams.OrderBy(t => t.Name).ToList();
            return teamsOrderedByName;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsOrderedByLocation()
        {
            var teams = await _context.Teams.ToListAsync();
            var teamsOrderedByLocation = teams.OrderBy(t => t.Location).ToList();
            return teamsOrderedByLocation;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<Player>>> GetPlayersOnTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.Players).SingleOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            if (team.Players == null)
            {
                return NotFound();
            }

            return team.Players;
        }

        [HttpPut("{playerId}/{teamId}")]
        public async Task<IActionResult> DeletePlayerOnTeam(int playerId, int teamId)
        {
            var team = _context.Teams.Include(t => t.Players).SingleOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }
            else
            {
                for (int i = 0; i < team.Players.Count; i++)
                {
                    if (team.Players[i].Id == playerId)
                    {
                        team.Players.RemoveAt(i);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(teamId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("{playerId}/{teamId}")]
        public async Task<IActionResult> AddPlayerToTeam(int playerId, int teamId)
        {
            var team = _context.Teams.Include(t => t.Players).SingleOrDefault(t => t.Id == teamId);

            var player = _context.Players.Find(playerId);

            if(team.Players == null)
            {
                team.Players.Add(player);
            }

            else if(team.Players.Contains(player) || team.Players.Count == 8)
            {
                return BadRequest();
            }

            else
            {
                team.Players.Add(player);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(teamId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            team.Players = new List<Player>();
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
