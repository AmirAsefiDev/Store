using Store.Application.Interfaces.Contexts;
using System.Collections.Generic;
using System.Linq;
using Store.Common;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(u => u.FullName.Contains(request.SearchKey) && u.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var userList = users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDto
            {
                Email = p.Email, 
                FullName = p.FullName,
                Id = p.Id,
                IsActive = p.IsActive
            }).ToList();

            return new ResultGetUserDto
            {
                Rows = rowsCount,
                Users = userList,
            };
        } 
    }
}
