using SnakeNet_API.DAL.Interfaces;
using SnakeNet_API.Models.Entities;

namespace SnakeNet_API.DAL
{
	// While obviously not needed for a project of this size and EF already have it built in, I wanted to try and implement a Unit of Work pattern and see how it goes
	/// <summary>
	/// One interface to rule them all repository interfaces
	/// </summary>
	public class UnitOfWork : IDisposable, IUnitOfWork
	{
		private readonly AppDbContext _context;
		private bool _disposed = false;

		private IGenericRepository<Snake> _snakeRepository;
		private IGenericRepository<Enclosure> _enclosureRepository;
		private IGenericRepository<GrowthRecord> _growthRecordRepository;
		private IGenericRepository<Elimination> _eliminationRepository;
		private IGenericRepository<FeedingRecord> _feedingRecordRepository;
		private IGenericRepository<EnclosureReading> _enclosureReadingRepository;
		private IGenericRepository<EnclosureLight> _enclosureLightRepository;
		private IGenericRepository<EnclosureSubstrate> _enclosureSubstrateRepository;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		// Properties exposing the repositories
		public IGenericRepository<Snake> SnakeRepository
		{
			get
			{
				if (_snakeRepository is null)
				{
					_snakeRepository = new GenericRepository<Snake>(_context);
				}
				return _snakeRepository;
			}
		}

		public IGenericRepository<Enclosure> EnclosureRepository
		{
			get
			{
				if (_enclosureRepository is null)
				{
					_enclosureRepository = new GenericRepository<Enclosure>(_context);
				}
				return _enclosureRepository;
			}
		}

		public IGenericRepository<GrowthRecord> GrowthRecordRepository
		{
			get
			{
				if (_growthRecordRepository is null)
				{
					_growthRecordRepository = new GenericRepository<GrowthRecord>(_context);
				}
				return _growthRecordRepository;
			}
		}

		public IGenericRepository<Elimination> EliminationRepository
		{
			get
			{
				if (_eliminationRepository is null)
				{
					_eliminationRepository = new GenericRepository<Elimination>(_context);
				}
				return _eliminationRepository;
			}
		}

		public IGenericRepository<FeedingRecord> FeedingRecordRepository
		{
			get
			{
				if (_feedingRecordRepository is null)
				{
					_feedingRecordRepository = new GenericRepository<FeedingRecord>(_context);
				}
				return _feedingRecordRepository;
			}
		}

		public IGenericRepository<EnclosureReading> EnclosureReadingRepository
		{
			get
			{
				if (_enclosureReadingRepository is null)
				{
					_enclosureReadingRepository = new GenericRepository<EnclosureReading>(_context);
				}
				return _enclosureReadingRepository;
			}
		}

		public IGenericRepository<EnclosureLight> EnclosureLightRepository
		{
			get
			{
				if (_enclosureLightRepository is null)
				{
					_enclosureLightRepository = new GenericRepository<EnclosureLight>(_context);
				}
				return _enclosureLightRepository;
			}
		}

		public IGenericRepository<EnclosureSubstrate> EnclosureSubstrateRepository
		{
			get
			{
				if (_enclosureSubstrateRepository is null)
				{
					_enclosureSubstrateRepository = new GenericRepository<EnclosureSubstrate>(_context);
				}
				return _enclosureSubstrateRepository;
			}
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		protected void Dispose(bool disposing)
		{
			if (!_disposed && disposing)
			{
				_context.Dispose();
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}