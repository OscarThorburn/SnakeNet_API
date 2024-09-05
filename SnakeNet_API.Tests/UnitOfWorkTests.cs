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
		public void UnitOfWork_LazyInitialization_RepositoriesAreNullBeforeAccess()
		{
			Assert.Null(typeof(UnitOfWork).GetField("_enclosureRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_growthRecordRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_eliminationRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_feedingRecordRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_enclosureReadingRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_enclosureLightRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
			Assert.Null(typeof(UnitOfWork).GetField("_enclosureSubstrateRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_unitOfWork));
		}

		[Fact]
		public void SnakeRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.SnakeRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<Snake>>(repository);
		}

		[Fact]
		public void EnclosureRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.EnclosureRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<Enclosure>>(repository);
		}

		[Fact]
		public void GrowthRecordRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.GrowthRecordRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<GrowthRecord>>(repository);
		}

		[Fact]
		public void EliminationRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.EliminationRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<Elimination>>(repository);
		}

		[Fact]
		public void FeedingRecordRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.FeedingRecordRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<FeedingRecord>>(repository);
		}

		[Fact]
		public void EnclosureReadingRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.EnclosureReadingRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<EnclosureReading>>(repository);
		}

		[Fact]
		public void EnclosureLightRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.EnclosureLightRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<EnclosureLight>>(repository);
		}

		[Fact]
		public void EnclosureSubstrateRepository_ReturnsInstanceOfGenericRepository()
		{
			var repository = _unitOfWork.EnclosureSubstrateRepository;

			Assert.NotNull(repository);
			Assert.IsType<GenericRepository<EnclosureSubstrate>>(repository);
		}

		[Fact]
		public async void SaveAsync_CallsSaveChangesAsyncOnContext()
		{
			await _unitOfWork.SnakeRepository.InsertAsync(new Snake { Id = "UnitOfWorkSave", Name = "Test1" });

			var result = await _unitOfWork.SaveAsync();

			Assert.Equal(1, result);
		}

		[Fact]
		public async Task UnitOfWork_AccessMultipleRepositories_WorksCorrectly()
		{
			var enclosure = new Enclosure
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 100,
				Height = 50,
				Depth = 50
			};
			var growthRecord = new GrowthRecord
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 120,
				Weight = 1000,
				Date = DateTime.Now,
				Snake = new Snake { Id = Guid.NewGuid().ToString(), Name = "TestSnake" }
			};

			await _unitOfWork.EnclosureRepository.InsertAsync(enclosure);
			await _unitOfWork.GrowthRecordRepository.InsertAsync(growthRecord);

			await _unitOfWork.SaveAsync();

			var savedEnclosure = await _unitOfWork.EnclosureRepository.GetByIDAsync(enclosure.Id);
			var savedGrowthRecord = await _unitOfWork.GrowthRecordRepository.GetByIDAsync(growthRecord.Id);

			Assert.NotNull(savedEnclosure);
			Assert.NotNull(savedGrowthRecord);
		}

		[Fact]
		public async Task EnclosureRepository_CanUpdateEntity()
		{
			var enclosure = new Enclosure
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 100,
				Height = 50,
				Depth = 50
			};

			await _unitOfWork.EnclosureRepository.InsertAsync(enclosure);
			await _unitOfWork.SaveAsync();

			enclosure.Lenght = 120;
			await _unitOfWork.EnclosureRepository.UpdateAsync(enclosure);
			await _unitOfWork.SaveAsync();

			var updatedEnclosure = await _unitOfWork.EnclosureRepository.GetByIDAsync(enclosure.Id);

			Assert.NotNull(updatedEnclosure);
			Assert.Equal(120, updatedEnclosure.Lenght);
		}

		[Fact]
		public async Task EnclosureRepository_CanDeleteEntity()
		{
			var enclosure = new Enclosure
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 100,
				Height = 50,
				Depth = 50
			};

			await _unitOfWork.EnclosureRepository.InsertAsync(enclosure);
			await _unitOfWork.SaveAsync();

			await _unitOfWork.EnclosureRepository.DeleteAsync(enclosure.Id);
			await _unitOfWork.SaveAsync();

			var deletedEnclosure = await _unitOfWork.EnclosureRepository.GetByIDAsync(enclosure.Id);

			Assert.Null(deletedEnclosure);
		}
	}
}
