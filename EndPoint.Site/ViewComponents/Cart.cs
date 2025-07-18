using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;

namespace EndPoint.Site.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookieManager _cookieManager;

        public Cart(ICartService cartService, CookieManager cookieManager)
        {
            _cartService = cartService;
            _cookieManager = cookieManager;
        }

        public IViewComponentResult Invoke()
        {
            var browserId = _cookieManager.GetBrowserId(HttpContext);
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View("Cart", _cartService.GetMyCart(browserId, userId).Data);
        }
    }
}