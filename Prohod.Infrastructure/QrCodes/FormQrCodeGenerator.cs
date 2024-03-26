using Prohod.Domain.Forms;
using QRCoder;

namespace Prohod.Infrastructure.QrCodes;

public class FormQrCodeGenerator : IFormQrCodeGenerator
{
    public string GenerateBase64QrCode(Form form)
    {
        var generator = new QRCodeGenerator();
        var html =
            $"Passport = {form.Passport}, " +
            $"VisitTime = {form.VisitTime}, " +
            $"UserToVisit = {form.UserToVisit.Name + " " + form.UserToVisit.Surname}";
        var data = generator.CreateQrCode(html, QRCodeGenerator.ECCLevel.Q);
        return new Base64QRCode(data).GetGraphic(20);
    }
}