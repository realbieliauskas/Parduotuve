using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories;

public class SkinRepository : ISkinRepository
{
    private readonly StoreDataContext _context;

    public SkinRepository(StoreDataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Skin>> GetAllAsync()
    {
        return await _context.Skins
            .Include(skin => skin.ChromaList)
            .ToListAsync();
    }

    public async Task<Skin?> GetByIdAsync(int id)
    {
        return await _context.Skins
            .Include(skin => skin.ChromaList)
            .FirstOrDefaultAsync(skin => skin.Id == id);
    }

    public async Task AddAsync(Skin skin)
    {
        await _context.Skins.AddAsync(skin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Skin skin)
    {
        _context.Skins.Update(skin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Skin? skin = await _context.Skins.FindAsync(id);
        if (skin != null)
        {
            _context.Skins.Remove(skin);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Skin>> GetSortedSkinsAsync(string sortBy)
    {
        IQueryable<Skin> query = _context.Skins.Include(skin => skin.ChromaList);

        query = sortBy switch
        {
            "ChampionName" => query.OrderBy(skin => skin.ChampionName.ToLower()),
            "Price" => query.OrderBy(skin => skin.Price),
            "Name" => query.OrderBy(skin => skin.Name.ToLower()),
            _ => query.OrderBy(skin => skin.ChampionName.ToLower())
        };

        return await query.ToListAsync();
    }
}