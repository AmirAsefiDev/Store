using System.Threading.Tasks;

namespace Store.Infrastructure.Zarinpal
{
    public interface IZarinpalPaymentService
    {
        Task<ZarinpalPaymentResponse?> RequestPaymentAsync(int amount, string callbackUrl, string description,
            string email,
            string mobile);
    }
}