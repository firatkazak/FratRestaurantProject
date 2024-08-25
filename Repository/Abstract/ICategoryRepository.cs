using FratRestaurantProject.Data.Entities;

namespace FratRestaurantProject.Repository.Abstract;

public interface ICategoryRepository
{
    public Task<Category> CreateAsync(Category obj);
    public Task<Category> UpdateAsync(Category obj);
    public Task<bool> DeleteAsync(int id);
    public Task<Category> GetAsync(int id);
    public Task<IEnumerable<Category>> GetAllAsync();
}
