using SnakeNet_API.DataAccess;
using SnakeNet_API.Models.Entities;

namespace SnakeNet_API.Data
{
	public class SqlDataService
	{
		private readonly AppDbContext _context;

        public SqlDataService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddSnake(Snake snake)
		{
			await _context.Snakes.AddAsync(snake);
			await _context.SaveChangesAsync();
		}

		public async Task AddEnclosure(Enclosure enclosure)
		{
			await _context.Enclosures.AddAsync(enclosure);
			await _context.SaveChangesAsync();
		}

		public async Task AddGrowthRecord(GrowthRecord growthRecord)
		{
			await _context.GrowthRecords.AddAsync(growthRecord);
			await _context.SaveChangesAsync();
		}

		public async Task AddElimination(Elimination elimination)
		{
			await _context.Eliminations.AddAsync(elimination);
			await _context.SaveChangesAsync();
		}

		public async Task AddEnclosureReading(EnclosureReading enclosureReading)
		{
			await _context.EnclosureReadings.AddAsync(enclosureReading);
			await _context.SaveChangesAsync();
		}
    }
}
