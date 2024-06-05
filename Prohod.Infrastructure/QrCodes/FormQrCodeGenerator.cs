using Prohod.Domain.Forms;
using QRCoder;

namespace Prohod.Infrastructure.QrCodes;

public class FormQrCodeGenerator : IFormQrCodeGenerator
{
    private readonly QRCodeGenerator generator = new();

    public string GenerateBase64QrCode(Guid visitRequestId)
    {
        var html = visitRequestId.ToString();
        var data = generator.CreateQrCode(html, QRCodeGenerator.ECCLevel.Q);
        return new Base64QRCode(data).GetGraphic(20);
    }
}