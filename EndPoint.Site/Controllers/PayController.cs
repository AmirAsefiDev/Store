using System;
using System.Threading.Tasks;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using Store.Application.Services.Finances.CreateRequestPay;
using Store.Application.Services.Finances.GetRequestPay;
using Store.Application.Services.Orders.AddNewOrder;
using Store.Infrastructure.Zarinpal;
using ZarinPal.Class;

namespace EndPoint.Site.Controllers;

[Authorize("Customer")]
public class PayController : Controller
{
    private static readonly Expose _expose;
    private readonly IAddNewOrderService _addNewOrderService;
    private readonly Authority _authority;

    private readonly ICartService _cartService;
    private readonly CookieManager _cookieManager;
    private readonly ICreateRequestPayService _createRequestPayService;
    private readonly IGetRequestPayService _getRequestPayService;
    private readonly Payment _payment;
    private readonly Transactions _transactions;
    private readonly IZarinpalPaymentService _zarinpalPaymentService;

    public PayController(
        ICreateRequestPayService createRequestPayService,
        ICartService cartService,
        IZarinpalPaymentService zarinpalPaymentService,
        IGetRequestPayService getRequestPayService,
        IAddNewOrderService addNewOrderService
    )
    {
        _createRequestPayService = createRequestPayService;
        _cookieManager = new CookieManager();
        _cartService = cartService;
        _zarinpalPaymentService = zarinpalPaymentService;
        _getRequestPayService = getRequestPayService;
        _addNewOrderService = addNewOrderService;
        var expose = new Expose();
        _payment = expose.CreatePayment();
        _authority = expose.CreateAuthority();
        _transactions = expose.CreateTransactions();
    }

    public async Task<IActionResult> Index()
    {
        var userId = ClaimUtility.GetUserId(User);
        var cart = _cartService.GetMyCart(_cookieManager.GetBrowserId(HttpContext), userId);
        if (cart.Data.SumAmount > 0)
        {
            var addResult = await _createRequestPayService.ExecuteAsync(cart.Data.SumAmount, userId.Value);
            //ارسال به درگاه پرداخت

            //var result = await _payment.Request(new DtoRequest
            //{
            //    Mobile = "09121112222",
            //    CallbackUrl = "https://localhost:44388/pay/verify",
            //    Description = "توضیحات",
            //    Email = "farazmaan@outlook.com",
            //    Amount = 1000000,
            //    MerchantId = "00000000-0000-0000-0000-000000000000"
            //}, Payment.Mode.sandbox);
            //return Json(result);
            //return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

            var result = await _zarinpalPaymentService.RequestPaymentAsync
            (
                1000000,
                $"https://localhost:44388/pay/verify?guid={addResult.Data.Guid}",
                "توضیحات",
                "farazmaan@outlook.com",
                "09121112222"
            );

            if (result?.Data?.Code == 100)
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result?.Data?.Authority}");
            return Content("درخواست پرداخت تست ناموفق");
        }


        return RedirectToAction("Index", "Cart");
    }

    public async Task<IActionResult> Verify(Guid guid, string authority)
    {
        var requestPay = await _getRequestPayService.ExecuteAsync(guid);
        if (requestPay.IsSuccess)
            // این کد کار نمیکند برای کامنت شده و برای تکمیل پرداخت باید همچین کدی داشته باشی.
            //var verification = await _payment.Verification(new DtoVerification
            //{
            //    Amount = (int)requestPay.Data.Amount,
            //    MerchantId = "ee7dc1ff-9a7e-40ab-8b42-46c8aea2514c", //این کد را لازم از زرین پال دریافت کنی
            //    Authority = authority
            //}, Payment.Mode.sandbox);

            //اگر کد های بالا کار نکردند میتوانی از کد هم برای درخواست زدن به API مورد زرین پال استفاده کنی امیر
            //var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{10000}\"}}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //VerificationPayResultDto verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);
            //if (verification.Status == 100)
            if (true)
            {
                var userId = ClaimUtility.GetUserId(User);
                var cart = _cartService.GetMyCart
                    (_cookieManager.GetBrowserId(HttpContext), userId);
                await _addNewOrderService.ExecuteAsync(new RequestAddNewOrder
                {
                    CartId = cart.Data.CartId,
                    UserId = userId.Value,
                    RequestPayId = requestPay.Data.Id,
                    Authority = authority,
                    RefId = 1111 // نمیدونیم این مقدار رو باید از کجا باید بگیریم.
                });

                //Redirect to orders
                return RedirectToAction("Index", "Order");
            }


        return View();
    }
}