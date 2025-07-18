using System;
using Store.Common.Dto;

namespace Store.Application.Services.Carts;

public interface ICartService
{
    ResultDto AddToCart(long productId, Guid browserId);
    ResultDto RemoveFromCart(long productId, Guid browserId);
    ResultDto<CartDto> GetMyCart(Guid browserId, long? userId);

    ResultDto Add(long cartItemId);
    ResultDto LowOff(long cartItemId);
}