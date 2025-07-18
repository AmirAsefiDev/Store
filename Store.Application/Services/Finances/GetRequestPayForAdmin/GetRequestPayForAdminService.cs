using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Finances.GetRequestPayForAdmin;

public class GetRequestPayForAdminService(IDataBaseContext context) : IGetRequestPayForAdminService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<List<GetRequestPayForAdminDto>>> ExecuteAsync()
    {
        var requestPays = await _context.RequestPays
            .Select(r => new GetRequestPayForAdminDto
            {
                Id = r.Id,
                Guid = r.Guid,
                UserId = r.UserId,
                Amount = r.Amount,
                IsPay = r.IsPay,
                PayDate = r.PayDate,
                Authority = r.Authority,
                RefId = r.RefId,
                UserName = r.User.FullName
            }).ToListAsync();

        return new ResultDto<List<GetRequestPayForAdminDto>>
        {
            Data = requestPays,
            IsSuccess = true
        };
    }
}