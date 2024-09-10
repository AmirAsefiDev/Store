using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(RequestEdituserDto request);
    }

    public class RequestEdituserDto
    {
        public long UserId { get; set; }    
        public string FullName { get; set; }
    }


}