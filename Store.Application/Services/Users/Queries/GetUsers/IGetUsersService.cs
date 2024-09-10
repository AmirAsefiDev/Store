using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        public ResultGetUserDto Execute(RequestGetUserDto request);
    }
}
