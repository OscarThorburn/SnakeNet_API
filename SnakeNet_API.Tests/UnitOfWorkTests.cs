using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using SnakeNet_API.DAL.Interfaces;

namespace SnakeNet_API.Tests
{
	public class UnitOfWorkTests : IClassFixture<DatabaseFixture>
	{
		private readonly AppDbContext _context;
		private readonly UnitOfWork _unitOfWork;

		public UnitOfWorkTests(DatabaseFixture fixture)
		{
			_context = fixture.Context;
			_unitOfWork = new UnitOfWork(_context);
		}

		[Fact]
		public void SnakeRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.SnakeRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<Snake>>(repository);
		}

		[Fact]
		public async void SaveAsync_CallsSaveChangesAsyncOnContext()
		{
			await _unitOfWork.SnakeRepository.InsertAsync(new Snake { Id = "UnitOfWorkSave", Name = "Test1" });

			var result = await _unitOfWork.SaveAsync();

			Assert.Equal(1, result);
		}
	}
}
