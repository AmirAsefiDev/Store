using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.UserSatusChange
{
    public class UserSatusChangeService : IUserSatusChangeService
    {
        private readonly IDataBaseContext _context;
        public UserSatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long UserId)
        {
            var user = _context.Users.Find(UserId);
            if(user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربری یافت نشد",
                };
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            string userstate = user.IsActive == true ? "فعال" : "غیر فعال";
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"کاربر با موفقیت {userstate} شد!"
            };
        }
    }
}
