using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace Prohod.Infrastructure.QrCodes;

public class EmailQrCodeSender : IEmailQrCodeSender
{
    public async Task SendAsync(string base64QrCode, string email)
    {
        var message = new MimeMessage();
        var from = new MailboxAddress("back", "prohod@prohod.com");
        var to = new MailboxAddress("user", email);
        message.From.Add(from);
        message.To.Add(to);
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $"""<img src="data:image/png;base64, {base64QrCode}" width="400" height="400"/>""";
        message.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
        
        await smtpClient.ConnectAsync("app.debugmail.io", 9025);

        await smtpClient.AuthenticateAsync(
            Encoding.UTF8,
            "3db54046-29d8-4254-b3d2-9ad868022356",
            "e5a65b2e-d39f-4bf2-97fd-aa87740bcbbc");

        await smtpClient.SendAsync(message);
        
        await smtpClient.DisconnectAsync(true);
    }
}