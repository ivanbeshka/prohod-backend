using Prohod.Domain.Forms;
using Prohod.Domain.QrCodes;
using Prohod.Domain.VisitRequests;

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
    
    public async Task CreateAndSendQrCodeAsync(VisitRequest visitRequest)
    {
        await codeSender.SendAsync(codeGenerator.GenerateBase64QrCode(visitRequest.Id), visitRequest.Form.EmailToSendReply);
    }
}