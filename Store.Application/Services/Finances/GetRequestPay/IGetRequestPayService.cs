using System;
using System.Threading.Tasks;
using Store.Common.Dto;

namespace Store.Application.Services.Finances.GetRequestPay;

public interface IGetRequestPayService
{
    Task<ResultDto<RequestPayDto>> ExecuteAsync(Guid guid);
}