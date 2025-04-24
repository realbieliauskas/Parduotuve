using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories;

public interface IChromaRepository
{
    Task<IEnumerable<Chroma>> GetAllAsync();
    Task<Chroma?> GetByIdAsync(int id);
    Task AddAsync(Chroma chroma);
    Task UpdateAsync(Chroma chroma);
    Task DeleteAsync(int id);
    Task<Chroma> GetLast();
}