using System.Threading.Tasks;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Queries.GetUserById;

public interface IGetUserByIdService
{
    Task<ResultDto<GetUsersDto>> ExecuteAsync(long userId);
}