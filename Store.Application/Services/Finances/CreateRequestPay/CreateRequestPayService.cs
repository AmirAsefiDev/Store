using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Finances;

namespace Store.Application.Services.Finances.CreateRequestPay;

public class CreateRequestPayService(IDataBaseContext context) : ICreateRequestPayService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<RequestPayResultDto>> ExecuteAsync(decimal amount, long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        RequestPay requestPay = new()
        {
            Guid = Guid.NewGuid(),
            UserId = userId,
            User = user,
            Amount = amount,
            IsPay = false
        };
        await _context.RequestPays.AddAsync(requestPay);
        await _context.SaveChangesAsync();
        return new ResultDto<RequestPayResultDto>
        {
            IsSuccess = true,
            Data = new RequestPayResultDto
            {
                Guid = requestPay.Guid
            },
            Message = "درخواست پرداخت با موفقیت ثبت شد."
        };
    }
}