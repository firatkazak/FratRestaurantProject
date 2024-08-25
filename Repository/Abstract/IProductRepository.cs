﻿using FratRestaurantProject.Data.Entities;

namespace FratRestaurantProject.Repository.Abstract;

public interface IProductRepository
{
    public Task<Product> CreateAsync(Product obj);
    public Task<Product> UpdateAsync(Product obj);
    public Task<bool> DeleteAsync(int id);
    public Task<Product> GetAsync(int id);
    public Task<IEnumerable<Product>> GetAllAsync();
}