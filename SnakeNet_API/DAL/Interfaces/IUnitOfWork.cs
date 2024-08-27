using SnakeNet_API.Models.Entities;

namespace SnakeNet_API.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Elimination> EliminationRepository { get; }
        IGenericRepository<EnclosureReading> EnclosureReadingRepository { get; }
        IGenericRepository<Enclosure> EnclosureRepository { get; }
        IGenericRepository<FeedingRecord> FeedingRecordRepository { get; }
        IGenericRepository<GrowthRecord> GrowthRecordRepository { get; }
        IGenericRepository<Snake> SnakeRepository { get; }

        void Dispose();
        Task<int> SaveAsync();
    }
}