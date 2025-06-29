using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Pdv.Infraestructure.Data.Context;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;


namespace API_Pdv.Infraestructure.Repositories;
public class MotoboyRepository : IMotoboy
{
    private readonly ApplicationDbContext _context;
    public MotoboyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MotoBoyEntity> GetByIdAsync(int id)
    {
        return await _context.Set<MotoBoyEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<MotoBoyEntity>> GetAllAsync()
    {
        return await _context.Set<MotoBoyEntity>().ToListAsync();
    }

    public async Task<MotoBoyEntity> CreateAsync(MotoBoyEntity motoboy)
    {
        _context.Set<MotoBoyEntity>().Add(motoboy);
        await _context.SaveChangesAsync();
        return motoboy;
    }

    public async Task<MotoBoyEntity> UpdateAsync(MotoBoyEntity motoboy)
    {
        _context.Set<MotoBoyEntity>().Update(motoboy);
        await _context.SaveChangesAsync();
        return motoboy;
    }

    public async Task DeleteAsync(int id)
    {
        var motoboy = await GetByIdAsync(id);
        if (motoboy != null)
        {
            _context.Set<MotoBoyEntity>().Remove(motoboy);
            await _context.SaveChangesAsync();
        }
    }
} 