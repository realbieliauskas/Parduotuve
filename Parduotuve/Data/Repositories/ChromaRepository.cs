using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories;

public class ChromaRepository : IChromaRepository
{
    private readonly StoreDataContext _context;

    public ChromaRepository(StoreDataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Chroma>> GetAllAsync()
    {
        return await _context.Chromas
            .Include(chroma => chroma.Skin)
            .ToListAsync();
    }

    public async Task<Chroma?> GetByIdAsync(int id)
    {
        return await _context.Chromas
            .Include(chroma => chroma.Skin)
            .FirstOrDefaultAsync(chroma => chroma.Id == id);
    }

    public async Task AddAsync(Chroma chroma)
    {
        await _context.Chromas.AddAsync(chroma);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Chroma chroma)
    {
        _context.Chromas.Update(chroma);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Chroma? chroma = await _context.Chromas.FindAsync(id);
        if (chroma != null)
        {
            _context.Chromas.Remove(chroma);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Chroma> GetLast()
    {
        return await _context.Chromas.OrderBy(a => a.Id)
            .Include(chroma => chroma.Skin)
            .LastAsync();
    }
}