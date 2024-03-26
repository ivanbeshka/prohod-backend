using Prohod.Domain.Forms;
using Prohod.Domain.QrCodes;

namespace Prohod.Infrastructure.QrCodes;

public class QrCodesService : IQrCodesService
{
    private readonly IEmailQrCodeSender codeSender;
    private readonly IFormQrCodeGenerator codeGenerator;

    public QrCodesService(IEmailQrCodeSender codeSender, IFormQrCodeGenerator codeGenerator)
    {
        this.codeSender = codeSender;
        this.codeGenerator = codeGenerator;
    }
    
    public async Task CreateAndSendQrCodeAsync(Form form)
    {
        await codeSender.SendAsync(codeGenerator.GenerateBase64QrCode(form), form.EmailToSendReply);
    }
}