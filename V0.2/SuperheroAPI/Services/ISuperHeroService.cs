using Microsoft.AspNetCore.Mvc;

namespace SuperheroAPI.Services
{
    public interface ISuperHeroService
    {
        Task<List<SuperHero>> GetHeroes();
        Task<SuperHero> GetHero(int id);
        Task<SuperHero> AddHero(SuperHero hero);
        Task<SuperHero> UpdateHero(int id, SuperHero req);
        Task<SuperHero> RemoveHero(int id);

    }
}
