using Microsoft.EntityFrameworkCore;
using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNet_API.Tests
{
	public class GenericRepositoryTests : IClassFixture<DatabaseFixture>
	{
		private readonly AppDbContext _context;
		private readonly GenericRepository<Snake> _repository;

		public GenericRepositoryTests(DatabaseFixture fixture)
		{
			_context = fixture.Context;
			_repository = new GenericRepository<Snake>(_context);
		}

		[Fact]
		public async Task GetAsync_ReturnsAllItems()
		{
			var result = await _repository.GetAsync();

			Assert.Equal(3, result.Count());
		}

		[Fact]
		public async Task GetAsync_WithQueryFilter_ReturnsAllFemale()
		{
			var result = await _repository.GetAsync(snake => snake.Sex == Models.Enums.Sex.Female);

			Assert.Equal(2, result.Count());
		}

		[Fact]
		public async Task InsertAsync_AddsEntityToDbSet()
		{
			var entity = new Snake { Id = Guid.NewGuid().ToString(), Name = "DummySnake" };

			await _repository.InsertAsync(entity);
			await _context.SaveChangesAsync();

			var snakeInDb = await _context.Snakes.FindAsync(entity.Id);
			Assert.NotNull(snakeInDb);
		}

	}
}
