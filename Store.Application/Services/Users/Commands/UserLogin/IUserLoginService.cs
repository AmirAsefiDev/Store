using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Common.Dto;
using System.Linq;

namespace Store.Application.Services.Users.Commands.UserLogin
{
    public interface IUserLoginService
    {
        ResultDto<ResultUserLoginDto> Execute(string Username, string Password);
    }
    public class UserLoginService : IUserLoginService
    {
        private readonly IDataBaseContext _context;
        public UserLoginService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserLoginDto> Execute(string Username, string Password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "نام کاربری و رمز عبور را وارد نمایید",
                };
            }

            var user = _context.Users.Include(p => p.UserInRoles)
                .ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(Username)
              && p.IsActive == true)
                .FirstOrDefault();

            if (user == null)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "کاربری با این ایمیل در سایت ثبت نام نکرده",
                };
            }
            var passwordHasher = new PasswordHasher();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, Password);
            if (resultVerifyPassword == false)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "رمز وارد شده اشتباه است!",
                };


            }
            var roles = "";
            foreach (var item in user.UserInRoles)
            {
                roles += $"{item.Role.Name}";
            }

            return new ResultDto<ResultUserLoginDto>()
            {
                Data = new ResultUserLoginDto()
                {
                    Roles = roles,
                    UserId = user.Id,
                    Name = user.FullName
                },
                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد",
            };
        }
    }

    public class ResultUserLoginDto
    {
        public long UserId { get; set; }
        public string Roles { get; set; }
        public string Name { get; set; }
    }
}
