using FratRestaurantProject.Data.Entities;
using FratRestaurantProject.Data;
using FratRestaurantProject.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FratRestaurantProject.Repository.Concrete;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Product> CreateAsync(Product obj)
    {
        await _db.Products.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Product obj = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        if (obj != null)
        {
            _db.Products.Remove(obj);
            return (await _db.SaveChangesAsync()) > 0;
        }
        return false;
    }

    public async Task<Product> GetAsync(int id)
    {
        Product obj = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);

        if (obj == null)
        {
            return new Product();
        }
        return obj;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.Include(u => u.Category).ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        Product objFromDb = await _db.Products.FirstOrDefaultAsync(u => u.Id == obj.Id);

        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.ImageUrl = obj.ImageUrl;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Price = obj.Price;
            _db.Products.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }
        return obj;
    }
}
