using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.GetUserOrder;

public interface IGetUserOrderService
{
    Task<ResultDto<List<GetUserOrderDto>>> ExecuteAsync(long userId);
}