using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Commands.UserSatusChange;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUsers;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserSatusChangeService _userSatusChangeService;
        private readonly IEditUserService _editUserService;

        public UsersController(IGetUsersService getUsersService,IGetRolesService getRolesService,IRegisterUserService registerUserService, IRemoveUserService removeUserService, IUserSatusChangeService userSatusChangeService, IEditUserService editUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _userSatusChangeService = userSatusChangeService;
            _editUserService = editUserService;
        }

        public IActionResult Index(string searchKey,int page=1)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto
            {
                Page = page,
                SearchKey = searchKey,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data ,"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string email, string fullName, int roleId,string password,string rePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDto
            {
                Email = email,
                FullName = fullName,
                roles = new List<RolesInResultRegisterDto>()
                {
                   new RolesInResultRegisterDto
                   {
                       Id = roleId
                   }
                },
                Password = password,
                RePassword = rePassword,
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserSatusChange(long UserId)
        {
            return Json(_userSatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId,string FullName)
        {
            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                FullName = FullName,
                UserId = UserId
            }));
        }
    }

}
