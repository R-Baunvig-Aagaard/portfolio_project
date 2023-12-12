
using SuperheroAPI.Data;

namespace SuperheroAPI.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private static List<SuperHero> superHeroes = new List<SuperHero>
            {
                new SuperHero {Id = 1,
                    FirstName = "Peter",
                    LastName = "Parker",
                    Name = "Spider Man",
                    Place = "New York City"
                },
                new SuperHero {Id = 2,
                    FirstName = "Tony",
                    LastName = "Stark",
                    Name = "Iron Man",
                    Place = "Malibu"
                }
            };
        private readonly DataContext _context;

        public SuperHeroService(DataContext context)
        {
            _context = context;
        }
        public async Task<SuperHero> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return hero;
        }

        public async Task<SuperHero> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return null;

            return hero;
        }

        public async Task<List<SuperHero>> GetHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return heroes;
        }

        public async Task<SuperHero> RemoveHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return null;

            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();
            return hero;
        }

        public async Task<SuperHero> UpdateHero(int id, SuperHero req)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return null;

            hero.Name = req.Name;
            hero.FirstName = req.FirstName;
            hero.LastName = req.LastName;
            hero.Place = req.Place;

            await _context.SaveChangesAsync();
            
            return hero;
        }
    }
}
