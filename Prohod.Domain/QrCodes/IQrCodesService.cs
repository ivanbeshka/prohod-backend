using Prohod.Domain.VisitRequests;

namespace Prohod.Domain.QrCodes;

public interface IQrCodesService
{
    Task CreateAndSendQrCodeAsync(VisitRequest visitRequest);
}