using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;

namespace Store.Application.Services.Carts;

public class CartService(IDataBaseContext context) : ICartService
{
    private readonly IDataBaseContext _context = context;

    public ResultDto AddToCart(long productId, Guid browserId)
    {
        var cart = _context.Carts.FirstOrDefault(c => c.BrowserId == browserId && !c.Finished);
        if (cart == null)
        {
            var newCart = new Cart
            {
                BrowserId = browserId,
                Finished = false
            };
            _context.Carts.Add(newCart);
            _context.SaveChanges();
            cart = newCart;
        }

        var product = _context.Products.Find(productId);
        var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.CartId == cart.Id);
        if (cartItem != null)
        {
            cartItem.Count++;
        }
        else
        {
            var newCartItem = new CartItem
            {
                Cart = cart,
                Count = 1,
                Price = product.Price,
                Product = product,
                CartId = int.Parse(cart.Id.ToString())
            };
            _context.CartItems.Add(newCartItem);
            _context.SaveChanges();
        }

        return new ResultDto
        {
            IsSuccess = true,
            Message = $" محصول {product.Name} با موفقیت به سبد خرید شما اضافه شد."
        };
    }

    public ResultDto RemoveFromCart(long productId, Guid browserId)
    {
        var cartItem = _context.CartItems.FirstOrDefault(c => c.Cart.BrowserId == browserId);
        if (cartItem != null)
        {
            cartItem.IsRemoved = true;
            cartItem.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = " محصول از سبد خرید شما حذف شد."
            };
        }

        return new ResultDto
        {
            IsSuccess = false,
            Message = " محصول یافت نشد."
        };
    }

    public ResultDto Add(long cartItemId)
    {
        var cartItem = _context.CartItems.Find(cartItemId);
        cartItem.Count++;
        _context.SaveChanges();
        return new ResultDto
        {
            IsSuccess = true
        };
    }

    public ResultDto LowOff(long cartItemId)
    {
        var cartItem = _context.CartItems.Find(cartItemId);
        if (cartItem.Count <= 1)
            return new ResultDto
            {
                IsSuccess = false
            };
        cartItem.Count--;
        _context.SaveChanges();
        return new ResultDto
        {
            IsSuccess = true
        };
    }

    public ResultDto<CartDto> GetMyCart(Guid browserId, long? userId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(c => c.Product)
            .ThenInclude(x => x.ProductImage)
            .Where(c => c.BrowserId == browserId && c.Finished == false)
            .OrderByDescending(c => c.Id)
            .FirstOrDefault();

        if (cart == null)
            return new ResultDto<CartDto>
            {
                IsSuccess = false,
                Data = null
            };

        if (userId != null)
        {
            var user = _context.Users.Find(userId);
            cart.User = user;
            _context.SaveChanges();
        }

        return new ResultDto<CartDto>
        {
            Data = new CartDto
            {
                CartId = cart.Id,
                ProductCount = cart.CartItems.Count,
                SumAmount = (long)cart.CartItems.Sum(c => c.Price * c.Count),
                CartItems = cart.CartItems.Select(c => new CartItemDto
                {
                    Count = c.Count,
                    Price = c.Price,
                    Product = c.Product.Name,
                    Id = c.Id,
                    Images = c.Product.ProductImage.FirstOrDefault()?.Src ?? "",
                    ProductId = c.ProductId
                }).ToList()
            },
            IsSuccess = true
        };
    }
}