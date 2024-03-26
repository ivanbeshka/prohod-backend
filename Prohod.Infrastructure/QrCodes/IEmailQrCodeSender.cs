using QRCoder;

namespace Prohod.Infrastructure.QrCodes;

public interface IEmailQrCodeSender
{
    Task SendAsync(string base64QrCode, string email);
}