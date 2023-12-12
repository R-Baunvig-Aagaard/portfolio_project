using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperheroAPI.Models;
using SuperheroAPI.Services;


namespace SuperheroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public ISuperHeroService _SuperHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _SuperHeroService = superHeroService;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetHeroes()
        {
            return await _SuperHeroService.GetHeroes();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetHero(int id)
        {
            var hero = await _SuperHeroService.GetHero(id);

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            await _SuperHeroService.AddHero(hero);
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero req)
        {
            var result = await _SuperHeroService.UpdateHero(id, req);
            if (result == null)
                return BadRequest("No hero found");

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveHero(int id)
        {
            var result = await _SuperHeroService.RemoveHero(id);
            if (result == null)
                return BadRequest("No hero found");
            
            return NoContent();

        }

    }
}
