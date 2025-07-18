using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using EndPoint.Site.Models.ViewModels.AuthenticationViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.UserLogin;
using Store.Common.Dto;

namespace EndPoint.Site.Controllers;

public class AuthenticationController : Controller
{
    private readonly IRegisterUserService _registerUserService;
    private readonly IUserLoginService _userLoginService;

    public AuthenticationController(IRegisterUserService registerUserService, IUserLoginService userLoginService)
    {
        _registerUserService = registerUserService;
        _userLoginService = userLoginService;
    }

    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Signup(SignupViewModel request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.RePassword))
            return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
        if (User.Identity.IsAuthenticated)
            return Json(new ResultDto
            {
                IsSuccess = false,
                Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید"
            });
        if (request.Password != request.RePassword)
            return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
        if (request.Password.Length < 8)
            return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });

        var emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

        var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);

        if (!match.Success)
            return Json(new ResultDto { IsSuccess = false, Message = "ایمیل خودرا به درستی وارد نمایید" });

        var signupResult = _registerUserService.Execute(new RequestRegisterUserDto
        {
            Email = request.Email,
            FullName = request.FullName,
            Password = request.Password,
            RePassword = request.RePassword,
            roles = new List<RolesInResultRegisterDto>
            {
                new() { Id = 3 }
            }
        });
        if (signupResult.IsSuccess)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, signupResult.Data.UserId.ToString()),
                new(ClaimTypes.Email, request.Email),
                new(ClaimTypes.Role, "Customer")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(principal, properties);
        }


        return Json(signupResult);
    }

    [HttpGet]
    public IActionResult Signin(string Returnurl = "/")
    {
        ViewBag.url = Returnurl;
        return View();
    }

    [HttpPost]
    public IActionResult Signin(string Email, string Password, string url = "/")
    {
        var signupUserResult = _userLoginService.Execute(Email, Password);
        if (signupUserResult.IsSuccess)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, signupUserResult.Data.UserId.ToString()),
                new(ClaimTypes.Email, Email),
                new(ClaimTypes.Name, signupUserResult.Data.Name)
                //new Claim(ClaimTypes.Role, signupUserResult.Data.Roles.ToString()),
            };
            foreach (var role in signupUserResult.Data.Roles) claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(5)
            };
            HttpContext.SignInAsync(principal, properties);
        }

        return Json(signupUserResult);
    }

    public IActionResult SignOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}