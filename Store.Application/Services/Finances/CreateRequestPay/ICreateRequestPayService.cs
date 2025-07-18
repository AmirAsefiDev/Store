using System;
using System.Threading.Tasks;
using Store.Common.Dto;

namespace Store.Application.Services.Finances.CreateRequestPay;

public interface ICreateRequestPayService
{
    Task<ResultDto<RequestPayResultDto>> ExecuteAsync(decimal amount, long userId);
}

public class RequestPayResultDto
{
    public Guid Guid { get; set; }
}