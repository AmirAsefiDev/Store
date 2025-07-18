using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Common.Dto;

namespace Store.Application.Services.Finances.GetRequestPayForAdmin;

public interface IGetRequestPayForAdminService
{
    Task<ResultDto<List<GetRequestPayForAdminDto>>> ExecuteAsync();
}