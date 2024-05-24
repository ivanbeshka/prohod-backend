using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace Prohod.Infrastructure.QrCodes;

public class EmailQrCodeSender : IEmailQrCodeSender
{
    public async Task SendAsync(string base64QrCode, string email)
    {
        var message = new MimeMessage();
        var from = new MailboxAddress("back", "prohodsmtpclient@gmail.com");
        var to = new MailboxAddress("user", email);
        message.From.Add(from);
        message.To.Add(to);
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $"""<img src="data:image/png;base64, {base64QrCode}" width="400" height="400"/>""";
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
}