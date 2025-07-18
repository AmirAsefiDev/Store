using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Queries.GetUserById;

public class GetUserByIdService(IDataBaseContext context) : IGetUserByIdService
{
    private readonly IDataBaseContext _context = context;

    public async Task<ResultDto<GetUsersDto>> ExecuteAsync(long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return new ResultDto<GetUsersDto>
            {
                IsSuccess = false,
                Message = "کاربر مورد نظر پیدا نشد."
            };
        return new ResultDto<GetUsersDto>
        {
            Data = new GetUsersDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive
            },
            IsSuccess = true
        };
    }
}