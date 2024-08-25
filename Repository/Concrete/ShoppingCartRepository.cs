﻿using FratRestaurantProject.Data.Entities;
using FratRestaurantProject.Data;
using FratRestaurantProject.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FratRestaurantProject.Repository.Concrete;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;

    public ShoppingCartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ClearCartAsync(string userId)
    {
        var cartItems = await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
        _db.ShoppingCarts.RemoveRange(cartItems);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string userId)
    {
        return await _db.ShoppingCarts.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
    }

    public async Task<int> GetTotalCartCartCountAsync(string userId)
    {
        int cartCount = 0;
        var cartItems = await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();

        foreach (var item in cartItems)
        {
            cartCount += item.Count;
        }
        return cartCount;
    }

    public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
        if (cart == null)
        {
            cart = new ShoppingCart
            {
                UserId = userId,
                ProductId = productId,
                Count = updateBy
            };

            await _db.ShoppingCarts.AddAsync(cart);
        }
        else
        {
            cart.Count += updateBy;
            if (cart.Count <= 0)
            {
                _db.ShoppingCarts.Remove(cart);
            }
        }
        return await _db.SaveChangesAsync() > 0;
    }
}