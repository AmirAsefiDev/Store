using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly CookieManager _cookieManager;

        public CartController(ICartService cartService, CookieManager cookieManager)
        {
            _cartService = cartService;
            _cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var resultGetLst = _cartService.GetMyCart(_cookieManager.GetBrowserId(HttpContext), userId);
            return View(resultGetLst.Data);
        }

        public IActionResult AddToCart(long productId)
        {
            var guid = _cookieManager.GetBrowserId(HttpContext);
            var addResult = _cartService.AddToCart(productId, guid);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     اینجا جهت امنیت بیشتر که چک کنیم که آیا خود کاربره داره سبد خریدش رو کم زیاده میکنه یا نه
        ///     باید از کاربر BrowserId رو هم دریافت کنیم که همون Guid هست تا عملیات افزودن یا کم کردن صفحه خرید اصولی باشه.
        ///     اما ما اینجا این کار رو نکردیم.
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        public IActionResult Add(long cartItemId)
        {
            _cartService.Add(cartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult LowOff(long cartItemId)
        {
            _cartService.LowOff(cartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId, _cookieManager.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}