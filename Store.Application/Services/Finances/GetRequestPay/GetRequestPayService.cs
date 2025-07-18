using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Finances.GetRequestPay;

public class GetRequestPayService(IDataBaseContext context) : IGetRequestPayService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<RequestPayDto>> ExecuteAsync(Guid guid)
    {
        var requestPay = await _context.RequestPays.FirstOrDefaultAsync(r => r.Guid == guid);
        if (requestPay != null)
            return new ResultDto<RequestPayDto>
            {
                IsSuccess = true,
                Data = new RequestPayDto
                {
                    Amount = requestPay.Amount,
                    Id = requestPay.Id
                }
            };
        return new ResultDto<RequestPayDto>
        {
            IsSuccess = false,
            Data = new RequestPayDto
            {
                Amount = 0,
                Id = 0
            }
        };
    }
}