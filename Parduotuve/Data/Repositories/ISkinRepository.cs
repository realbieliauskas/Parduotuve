using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories
{
    public interface ISkinRepository
    {
        Task<IEnumerable<Skin>> GetAllAsync();
        Task<Skin?> GetByIdAsync(int id);
        Task AddAsync(Skin skin);
        Task UpdateAsync(Skin skin);
        Task DeleteAsync(int id);
        Task<IEnumerable<Skin>> GetSortedSkinsAsync(string sortBy);
    }

}
