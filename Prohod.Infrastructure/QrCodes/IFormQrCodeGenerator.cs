using Prohod.Domain.Forms;
using QRCoder;

namespace Prohod.Infrastructure.QrCodes;

public interface IFormQrCodeGenerator
{
    string GenerateBase64QrCode(Form form);
}