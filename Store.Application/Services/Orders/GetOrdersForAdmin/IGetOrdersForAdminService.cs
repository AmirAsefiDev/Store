using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.GetOrdersForAdmin;

public interface IGetOrdersForAdminService
{
    Task<ResultDto<List<OrdersDto>>> ExecuteAsync(OrderState orderState);
}