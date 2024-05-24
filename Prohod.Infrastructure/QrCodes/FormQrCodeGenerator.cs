using Prohod.Domain.Forms;
using QRCoder;

namespace Prohod.Infrastructure.QrCodes;

public class FormQrCodeGenerator : IFormQrCodeGenerator
{
    private readonly QRCodeGenerator generator = new();

    public string GenerateBase64QrCode(Form form)
    {
        var html =
            $"{form.Passport.FullName}\n" +
            $"Паспорт: {form.Passport.Series} {form.Passport.Number}\n" +
            $"Выдан: {form.Passport.IssueDate.ToString("dd.MM.yyyy")}\n" +
            $"Дата посещения: {form.VisitTime.ToString("dd.MM.yyyy")}\n\n" +
            $"Посещение одобрено";
        var data = generator.CreateQrCode(html, QRCodeGenerator.ECCLevel.Q);
        return new Base64QRCode(data).GetGraphic(20);
    }
}