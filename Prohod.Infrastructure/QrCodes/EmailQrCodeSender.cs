using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace Prohod.Infrastructure.QrCodes;

public class EmailQrCodeSender : IEmailQrCodeSender
{
    public async Task SendAsync(string base64QrCode, string email)
    {
        var message = new MimeMessage();
        var from = new MailboxAddress("Prohod", "prohodsmtpclient@gmail.com");
        var to = new MailboxAddress("User", email);
        message.From.Add(from);
        message.To.Add(to);
        message.Subject = "Пропуск в ИРИТ-РТФ";
        
        var bodyBuilder = new BodyBuilder();
        var contentId = bodyBuilder.LinkedResources.Add("qr.png", Base64ToImageStream(base64QrCode));
        bodyBuilder.HtmlBody = "";
        message.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync("smtp.gmail.com", 587);
        await smtpClient.AuthenticateAsync(
            Encoding.UTF8,
            "prohodsmtpclient@gmail.com",
            "qoci txqw eqzc yajn");
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }

    public static Stream Base64ToImageStream(string base64String)
    {
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        return ms;
    }
}