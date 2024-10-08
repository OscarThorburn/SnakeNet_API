using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using SnakeNet_API.Models.Enums;
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
		public void UnitOfWork_Repository_ReturnsInstanceOfGenericRepository(string repositoryName, Type expectedType)
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
		public async Task UnitOfWork_EnclosureReadingRepository_CanCreateAndReadEntity()
		{
			var newReading = new EnclosureReading
			{
				Id = Guid.NewGuid().ToString(),
				Temperature = 28,
				Humidity = 60,
				Date = DateTime.Now,
				EnclosureSide = EnclosureSide.HotLeft,
				Comment = "New Reading",
				Enclosure = _context.Enclosures.Find("208d16b6-1bb6-45be-940c-b99cec7acdd7") // Using seeded Enclosure 1
			};

			await _unitOfWork.EnclosureReadingRepository.InsertAsync(newReading);
			await _unitOfWork.SaveAsync();

			var savedReading = await _unitOfWork.EnclosureReadingRepository.GetByIDAsync(newReading.Id);

			Assert.NotNull(savedReading);
			Assert.Equal(28, savedReading.Temperature);
			Assert.Equal(60, savedReading.Humidity);
			Assert.Equal("New Reading", savedReading.Comment);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureReadingRepository_CanUpdateEntity()
		{
			var reading = await _unitOfWork.EnclosureReadingRepository.GetByIDAsync("08efe310-f318-468b-853c-9daa4d67bff0"); // Fetching seeded EnclosureReading 1
			Assert.NotNull(reading);

			reading.Temperature = 29;
			reading.Humidity = 65;
			reading.Comment = "Updated Reading";

			await _unitOfWork.EnclosureReadingRepository.UpdateAsync(reading);
			await _unitOfWork.SaveAsync();

			var updatedReading = await _unitOfWork.EnclosureReadingRepository.GetByIDAsync(reading.Id);

			Assert.NotNull(updatedReading);
			Assert.Equal(29, updatedReading.Temperature);
			Assert.Equal(65, updatedReading.Humidity);
			Assert.Equal("Updated Reading", updatedReading.Comment);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureReadingRepository_CanDeleteEntity()
		{
			var reading = await _unitOfWork.EnclosureReadingRepository.GetByIDAsync("08efe310-f318-468b-853c-9daa4d67bff0"); // Fetching seeded EnclosureReading 1
			Assert.NotNull(reading);

			await _unitOfWork.EnclosureReadingRepository.DeleteAsync(reading.Id);
			await _unitOfWork.SaveAsync();

			var deletedReading = await _unitOfWork.EnclosureReadingRepository.GetByIDAsync(reading.Id);

			Assert.Null(deletedReading);
		}

		// CRUD Tests for EnclosureSubstrate Entity
		[Fact]
		public async Task UnitOfWork_EnclosureSubstrateRepository_CanCreateAndReadEntity()
		{
			var newSubstrate = new EnclosureSubstrate
			{
				Id = Guid.NewGuid().ToString(),
				Name = "New Substrate",
				SubstrateType = SubstrateType.Sand,
				Manufacturer = "SubstrateCorp",
				Volume = 10,
				InUse = true,
				AddedDate = DateTime.Now,
				Enclosure = _context.Enclosures.Find("208d16b6-1bb6-45be-940c-b99cec7acdd7") // Using seeded Enclosure 1
			};

			await _unitOfWork.EnclosureSubstrateRepository.InsertAsync(newSubstrate);
			await _unitOfWork.SaveAsync();

			var savedSubstrate = await _unitOfWork.EnclosureSubstrateRepository.GetByIDAsync(newSubstrate.Id);

			Assert.NotNull(savedSubstrate);
			Assert.Equal("New Substrate", savedSubstrate.Name);
			Assert.Equal(SubstrateType.Sand, savedSubstrate.SubstrateType);
			Assert.Equal("SubstrateCorp", savedSubstrate.Manufacturer);
			Assert.Equal(10, savedSubstrate.Volume);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureSubstrateRepository_CanUpdateEntity()
		{
			var substrate = await _unitOfWork.EnclosureSubstrateRepository.GetByIDAsync("5c8227d5-fef0-49a4-8227-db34d8bbba60"); // Fetching seeded EnclosureSubstrate 1
			Assert.NotNull(substrate);

			substrate.Name = "Updated Substrate";
			substrate.Volume = 15;

			await _unitOfWork.EnclosureSubstrateRepository.UpdateAsync(substrate);
			await _unitOfWork.SaveAsync();

			var updatedSubstrate = await _unitOfWork.EnclosureSubstrateRepository.GetByIDAsync(substrate.Id);

			Assert.NotNull(updatedSubstrate);
			Assert.Equal("Updated Substrate", updatedSubstrate.Name);
			Assert.Equal(15, updatedSubstrate.Volume);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureSubstrateRepository_CanDeleteEntity()
		{
			var substrate = await _unitOfWork.EnclosureSubstrateRepository.GetByIDAsync("5c8227d5-fef0-49a4-8227-db34d8bbba60"); // Fetching seeded EnclosureSubstrate 1
			Assert.NotNull(substrate);

			await _unitOfWork.EnclosureSubstrateRepository.DeleteAsync(substrate.Id);
			await _unitOfWork.SaveAsync();

			var deletedSubstrate = await _unitOfWork.EnclosureSubstrateRepository.GetByIDAsync(substrate.Id);

			Assert.Null(deletedSubstrate);
		}

		// CRUD Tests for EnclosureLight Entity
		[Fact]
		public async Task UnitOfWork_EnclosureLightRepository_CanCreateAndReadEntity()
		{
			var newLight = new EnclosureLight
			{
				Id = Guid.NewGuid().ToString(),
				Name = "New LED Light",
				LightingType = LightingType.LED,
				Manufacturer = "LightCorp",
				Side = EnclosureSide.ColdRight,
				Wattage = 40,
				AddedDate = DateTime.Now,
				InUse = true,
				Enclosure = _context.Enclosures.Find("208d16b6-1bb6-45be-940c-b99cec7acdd7") // Using seeded Enclosure 1
			};

			await _unitOfWork.EnclosureLightRepository.InsertAsync(newLight);
			await _unitOfWork.SaveAsync();

			var savedLight = await _unitOfWork.EnclosureLightRepository.GetByIDAsync(newLight.Id);

			Assert.NotNull(savedLight);
			Assert.Equal("New LED Light", savedLight.Name);
			Assert.Equal(LightingType.LED, savedLight.LightingType);
			Assert.Equal(40, savedLight.Wattage);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureLightRepository_CanUpdateEntity()
		{
			var light = await _unitOfWork.EnclosureLightRepository.GetByIDAsync("1f508491-7592-41ac-8e4f-e3a9b583f147"); // Fetching seeded EnclosureLight 1
			Assert.NotNull(light);

			light.Name = "Updated Light";
			light.Wattage = 60;

			await _unitOfWork.EnclosureLightRepository.UpdateAsync(light);
			await _unitOfWork.SaveAsync();

			var updatedLight = await _unitOfWork.EnclosureLightRepository.GetByIDAsync(light.Id);

			Assert.NotNull(updatedLight);
			Assert.Equal("Updated Light", updatedLight.Name);
			Assert.Equal(60, updatedLight.Wattage);
		}

		[Fact]
		public async Task UnitOfWork_EnclosureLightRepository_CanDeleteEntity()
		{
			var light = await _unitOfWork.EnclosureLightRepository.GetByIDAsync("1f508491-7592-41ac-8e4f-e3a9b583f147"); // Fetching seeded EnclosureLight 1
			Assert.NotNull(light);

			await _unitOfWork.EnclosureLightRepository.DeleteAsync(light.Id);
			await _unitOfWork.SaveAsync();

			var deletedLight = await _unitOfWork.EnclosureLightRepository.GetByIDAsync(light.Id);

			Assert.Null(deletedLight);
		}
	}
}

