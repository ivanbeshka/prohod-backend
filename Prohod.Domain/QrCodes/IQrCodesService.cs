using Prohod.Domain.Forms;

namespace Prohod.Domain.QrCodes;

public interface IQrCodesService
{
    Task CreateAndSendQrCodeAsync(Form form);
}