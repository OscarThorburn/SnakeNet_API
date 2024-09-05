using Microsoft.EntityFrameworkCore;
using SnakeNet_API.DAL;
using SnakeNet_API.Models.Entities;
using SnakeNet_API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				Id = Guid.NewGuid().ToString(),
				Name = "Ragnar",
				Sex = Sex.Male
			};

			var snake2 = new Snake
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Stefan",
				Sex = Sex.Female
			};

			var enclosure1 = new Enclosure
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 100,
				Height = 50,
				Depth = 50,
				Snake = snake1
			};

			var enclosure2 = new Enclosure
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 120,
				Height = 60,
				Depth = 60,
				Snake = snake2
			};

			snake1.Enclosure = enclosure1;
			snake2.Enclosure = enclosure2;

			var light1 = new EnclosureLight
			{
				Id = Guid.NewGuid().ToString(),
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
				Id = Guid.NewGuid().ToString(),
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
				Id = Guid.NewGuid().ToString(),
				Temperature = 30,
				Humidity = 60,
				Date = DateTime.Now,
				EnclosureSide = EnclosureSide.ColdLeft,
				Comment = "Stable temperature and humidity",
				Enclosure = enclosure1
			};

			var reading2 = new EnclosureReading
			{
				Id = Guid.NewGuid().ToString(),
				Temperature = 32,
				Humidity = 65,
				Date = DateTime.Now,
				EnclosureSide = EnclosureSide.ColdRight,
				Comment = "Slightly higher temperature",
				Enclosure = enclosure2
			};

			var substrate1 = new EnclosureSubstrate
			{
				Id = Guid.NewGuid().ToString(),
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
				Id = Guid.NewGuid().ToString(),
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
				Id = Guid.NewGuid().ToString(),
				FeederWeight = 50,
				Feeder = Feeder.Mouse,
				Date = DateTime.Now.AddDays(-5),
				Comment = "Ate well",
				Snake = snake1
			};

			var feeding2 = new FeedingRecord
			{
				Id = Guid.NewGuid().ToString(),
				FeederWeight = 60,
				Feeder = Feeder.Rat,
				Date = DateTime.Now.AddDays(-7),
				Comment = "Refused food initially but ate later",
				Snake = snake2
			};

			var growth1 = new GrowthRecord
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 100,
				Weight = 500,
				Date = DateTime.Now.AddMonths(-1),
				Snake = snake1
			};

			var growth2 = new GrowthRecord
			{
				Id = Guid.NewGuid().ToString(),
				Lenght = 150,
				Weight = 700,
				Date = DateTime.Now.AddMonths(-2),
				Snake = snake2
			};

			var elimination1 = new Elimination
			{
				Id = Guid.NewGuid().ToString(),
				Date = DateTime.Now.AddDays(-3),
				Healthy = true,
				Type = EliminationType.Poop,
				Comment = "Normal",
				Snake = snake1
			};

			var elimination2 = new Elimination
			{
				Id = Guid.NewGuid().ToString(),
				Date = DateTime.Now.AddDays(-2),
				Healthy = false,
				Type = EliminationType.Shed,
				Comment = "Needed help",
				Snake = snake2
			};

			Context.Snakes.AddRange(snake1, snake2);
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
