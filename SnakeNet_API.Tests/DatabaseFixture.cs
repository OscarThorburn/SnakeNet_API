using Microsoft.EntityFrameworkCore;
using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using SnakeNet_API.Models.Enums;

namespace SnakeNet_API.Tests
{
	public class DatabaseFixture : IDisposable
	{
		public AppDbContext Context { get; private set; }

		public DatabaseFixture()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each instance
				.Options;

			Context = new AppDbContext(options);

			SeedData();
		}

		private void SeedData()
		{
			var snake1 = new Snake
			{
				Id = "7e30e84a-67d3-491d-98a5-ee3759892baa",
				Name = "Ragnar",
				Sex = Sex.Male
			};

			var snake2 = new Snake
			{
				Id = "97049c06-66bb-4c7e-989f-a4d212620eb9",
				Name = "Stefan",
				Sex = Sex.Female
			};

			var snake3 = new Snake
			{
				Id = "757f7c0d-6844-4693-96d7-126a04bc0dce",
				Name = "Bollman",
				Sex = Sex.Female
			};

			var enclosure1 = new Enclosure
			{
				Id = "208d16b6-1bb6-45be-940c-b99cec7acdd7",
				Lenght = 100,
				Height = 50,
				Depth = 50,
				Snake = snake1
			};

			var enclosure2 = new Enclosure
			{
				Id = "7393f89b-0584-41a7-be7b-fe719c29bd34",
				Lenght = 120,
				Height = 60,
				Depth = 60,
				Snake = snake2
			};

			snake1.Enclosure = enclosure1;
			snake2.Enclosure = enclosure2;

			var light1 = new EnclosureLight
			{
				Id = "1f508491-7592-41ac-8e4f-e3a9b583f147",
				Name = "UVB Light",
				LightingType = LightingType.UVB,
				Manufacturer = "ReptileCo",
				Side = EnclosureSide.HotLeft,
				Wattage = 25,
				AddedDate = DateTime.Now.AddDays(-30),
				InUse = true,
				Enclosure = enclosure1
			};

			var light2 = new EnclosureLight
			{
				Id = "3503fe67-3aac-43a0-a1d1-6383e8880d9a",
				Name = "Heat Lamp",
				LightingType = LightingType.Halogen,
				Manufacturer = "ReptileLight",
				Side = EnclosureSide.HotRight,
				Wattage = 50,
				AddedDate = DateTime.Now.AddDays(-10),
				InUse = true,
				Enclosure = enclosure2
			};

			var reading1 = new EnclosureReading
			{
				Id = "08efe310-f318-468b-853c-9daa4d67bff0",
				Temperature = 30,
				Humidity = 60,
				Date = DateTime.Now,
				EnclosureSide = EnclosureSide.ColdLeft,
				Comment = "Stable temperature and humidity",
				Enclosure = enclosure1
			};

			var reading2 = new EnclosureReading
			{
				Id = "26c50a5d-cfc9-4568-a7a4-522791c6e853",
				Temperature = 32,
				Humidity = 65,
				Date = DateTime.Now,
				EnclosureSide = EnclosureSide.ColdRight,
				Comment = "Slightly higher temperature",
				Enclosure = enclosure2
			};

			var substrate1 = new EnclosureSubstrate
			{
				Id = "5c8227d5-fef0-49a4-8227-db34d8bbba60",
				Name = "Cypress Mulch",
				SubstrateType = SubstrateType.CypressMulch,
				Manufacturer = "SubstrateCorp",
				Volume = 5,
				InUse = true,
				AddedDate = DateTime.Now.AddDays(-15),
				Enclosure = enclosure1
			};

			var substrate2 = new EnclosureSubstrate
			{
				Id = "ea61d1d5-121f-4de5-a3c5-5d4ddde41989",
				Name = "Aspen Shavings",
				SubstrateType = SubstrateType.Cocofiber,
				Manufacturer = "PetWorld",
				Volume = 8,
				InUse = true,
				AddedDate = DateTime.Now.AddDays(-20),
				Enclosure = enclosure2
			};

			var feeding1 = new FeedingRecord
			{
				Id = "c92ff237-bbc3-4f12-918c-5f1ef66b4a82",
				FeederWeight = 50,
				Feeder = Feeder.Mouse,
				Date = DateTime.Now.AddDays(-5),
				Comment = "Ate well",
				Snake = snake1
			};

			var feeding2 = new FeedingRecord
			{
				Id = "20730b18-0ce1-445d-a05b-c611b39a8932",
				FeederWeight = 60,
				Feeder = Feeder.Rat,
				Date = DateTime.Now.AddDays(-7),
				Comment = "Refused food initially but ate later",
				Snake = snake2
			};

			var growth1 = new GrowthRecord
			{
				Id = "b35bda98-5802-4a4d-97c5-1e5ed83453bf",
				Lenght = 100,
				Weight = 500,
				Date = DateTime.Now.AddMonths(-1),
				Snake = snake1
			};

			var growth2 = new GrowthRecord
			{
				Id = "47aa5159-ea9d-4ddf-b7b3-9ff23a8baf04",
				Lenght = 150,
				Weight = 700,
				Date = DateTime.Now.AddMonths(-2),
				Snake = snake2
			};

			var elimination1 = new Elimination
			{
				Id = "25683407-4070-47e9-a759-ef170b4ab6f5",
				Date = DateTime.Now.AddDays(-3),
				Healthy = true,
				Type = EliminationType.Poop,
				Comment = "Normal",
				Snake = snake1
			};

			var elimination2 = new Elimination
			{
				Id = "9e4ebc9b-0e94-402b-8271-c11c4f8e3f03",
				Date = DateTime.Now.AddDays(-2),
				Healthy = false,
				Type = EliminationType.Shed,
				Comment = "Needed help",
				Snake = snake2
			};

			Context.Snakes.AddRange(snake1, snake2, snake3);
			Context.Enclosures.AddRange(enclosure1, enclosure2);
			Context.EnclosureLights.AddRange(light1, light2);
			Context.EnclosureReadings.AddRange(reading1, reading2);
			Context.EnclosureSubstrates.AddRange(substrate1, substrate2);
			Context.FeedingRecords.AddRange(feeding1, feeding2);
			Context.GrowthRecords.AddRange(growth1, growth2);
			Context.Eliminations.AddRange(elimination1, elimination2);

			Context.SaveChanges();
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}
}
