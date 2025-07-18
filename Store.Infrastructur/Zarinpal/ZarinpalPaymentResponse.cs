namespace Store.Infrastructure.Zarinpal
{
    public class ZarinpalPaymentResponse
    {
        public ZarinpalPaymentData Data { get; set; }
        public object[] Errors { get; set; }
    }

    public class ZarinpalPaymentData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Authority { get; set; }
    }
}