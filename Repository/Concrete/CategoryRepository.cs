using FratRestaurantProject.Data.Entities;
using FratRestaurantProject.Data;
using FratRestaurantProject.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FratRestaurantProject.Repository.Concrete;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<Category> CreateAsync(Category obj)
    {
        await _db.Categories.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
        if (obj != null)
        {
            _db.Categories.Remove(obj);
            return (await _db.SaveChangesAsync()) > 0;
        }
        return false;
    }

    public async Task<Category> GetAsync(int id)
    {
        var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
        if (obj == null)
        {
            return new Category();
        }
        return obj;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<Category> UpdateAsync(Category obj)
    {
        var objFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            _db.Categories.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }
        return obj;
    }
}
