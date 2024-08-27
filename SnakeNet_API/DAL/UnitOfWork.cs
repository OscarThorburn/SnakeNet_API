using SnakeNet_API.DAL.Interfaces;
using SnakeNet_API.Models.Entities;

namespace SnakeNet_API.DAL
{
    /// <summary>
    /// While obviously not needed for this project size and EF already have it built in, I wanted to try and implement a Unit of Work pattern and see how it goes
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

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		// Properties exposing the repositories
		public IGenericRepository<Snake> SnakeRepository
		{
			get
			{
				if (_snakeRepository == null)
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
				if (_enclosureRepository == null)
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
				if (_growthRecordRepository == null)
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
				if (_eliminationRepository == null)
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
				if (_feedingRecordRepository == null)
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
				if (_enclosureReadingRepository == null)
				{
					_enclosureReadingRepository = new GenericRepository<EnclosureReading>(_context);
				}
				return _enclosureReadingRepository;
			}
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
