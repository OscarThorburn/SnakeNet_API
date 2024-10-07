using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using Xunit;

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

		[Theory]
		[InlineData("_enclosureRepository")]
		[InlineData("_growthRecordRepository")]
		[InlineData("_eliminationRepository")]
		[InlineData("_feedingRecordRepository")]
		[InlineData("_enclosureReadingRepository")]
		[InlineData("_enclosureLightRepository")]
		[InlineData("_enclosureSubstrateRepository")]
		public void UnitOfWork_LazyInitialization_RepositoryIsNullBeforeAccess(string repositoryField)
		{
			var fieldValue = typeof(UnitOfWork)
				.GetField(repositoryField, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
				.GetValue(_unitOfWork);

			Assert.Null(fieldValue);
		}

		[Theory]
		[InlineData("SnakeRepository", typeof(Snake))]
		[InlineData("EnclosureRepository", typeof(Enclosure))]
		[InlineData("GrowthRecordRepository", typeof(GrowthRecord))]
		[InlineData("EliminationRepository", typeof(Elimination))]
		[InlineData("FeedingRecordRepository", typeof(FeedingRecord))]
		[InlineData("EnclosureReadingRepository", typeof(EnclosureReading))]
		[InlineData("EnclosureLightRepository", typeof(EnclosureLight))]
		[InlineData("EnclosureSubstrateRepository", typeof(EnclosureSubstrate))]
		public void Repository_ReturnsInstanceOfGenericRepository(string repositoryName, Type expectedType)
		{
			var repository = typeof(UnitOfWork).GetProperty(repositoryName)?.GetValue(_unitOfWork);
			Assert.NotNull(repository);
			Assert.IsType(typeof(GenericRepository<>).MakeGenericType(expectedType), repository);
		}

		[Fact]
		public async Task SaveAsync_CallsSaveChangesAsyncOnContext()
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
