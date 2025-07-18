using System.Threading.Tasks;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.AddNewOrder;

public interface IAddNewOrderService
{
    Task<ResultDto> ExecuteAsync(RequestAddNewOrder request);
}