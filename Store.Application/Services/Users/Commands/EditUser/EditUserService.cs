using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;
        public EditUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEdituserDto request)
        {
            var user = _context.Users.Find(request.UserId);
            if(user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }

            user.FullName = request.FullName;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کاربر ویرایش شد"
            };
        }
    }


}
