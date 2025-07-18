using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Commands.UserSatusChange;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUserById;
using Store.Application.Services.Users.Queries.GetUsers;

namespace EndPoint.Site.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IEditUserService _editUserService;
    private readonly IGetRolesService _getRolesService;
    private readonly IGetUserByIdService _getUserByIdService;
    private readonly IGetUsersService _getUsersService;
    private readonly IRegisterUserService _registerUserService;
    private readonly IRemoveUserService _removeUserService;
    private readonly IUserSatusChangeService _userSatusChangeService;

    public UsersController(
        IGetUsersService getUsersService,
        IGetRolesService getRolesService,
        IRegisterUserService registerUserService,
        IRemoveUserService removeUserService,
        IUserSatusChangeService userSatusChangeService,
        IEditUserService editUserService,
        IGetUserByIdService getUserByIdService
    )
    {
        _getUsersService = getUsersService;
        _getRolesService = getRolesService;
        _registerUserService = registerUserService;
        _removeUserService = removeUserService;
        _userSatusChangeService = userSatusChangeService;
        _editUserService = editUserService;
        _getUserByIdService = getUserByIdService;
    }

    public IActionResult Index(string searchKey, int page = 1)
    {
        return View(_getUsersService.Execute(new RequestGetUserDto
        {
            Page = page,
            SearchKey = searchKey
        }));
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(string email, string fullName, int roleId, string password, string rePassword)
    {
        var result = _registerUserService.Execute(new RequestRegisterUserDto
        {
            Email = email,
            FullName = fullName,
            roles = new List<RolesInResultRegisterDto>
            {
                new()
                {
                    Id = roleId
                }
            },
            Password = password,
            RePassword = rePassword
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
    public IActionResult Edit(long UserId, string FullName)
    {
        return Json(_editUserService.Execute(new RequestEdituserDto
        {
            FullName = FullName,
            UserId = UserId
        }));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserById(long userId)
    {
        var getUser = await _getUserByIdService.ExecuteAsync(userId);
        if (!getUser.IsSuccess)
            return NotFound(getUser);
        return View(getUser.Data);
    }
}