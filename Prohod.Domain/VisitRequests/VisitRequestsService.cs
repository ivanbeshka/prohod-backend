using Kontur.Results;
using Prohod.Domain.ErrorsBase;
using Prohod.Domain.Forms;
using Prohod.Domain.GenericRepository;
using Prohod.Domain.QrCodes;
using Prohod.Domain.Users;

namespace Prohod.Domain.VisitRequests;

public class VisitRequestsService : IVisitRequestsService
{
    private readonly IVisitRequestsRepository visitRequestsRepository;
    private readonly IFormsRepository formsRepository;
    private readonly IUsersRepository usersRepository;
    private readonly IQrCodesService qrCodesService;

    public VisitRequestsService(
        IVisitRequestsRepository visitRequestsRepository,
        IFormsRepository formsRepository,
        IUsersRepository usersRepository,
        IQrCodesService qrCodesService)
    {
        this.visitRequestsRepository = visitRequestsRepository;
        this.formsRepository = formsRepository;
        this.usersRepository = usersRepository;
        this.qrCodesService = qrCodesService;
    }

    public async Task<Result<EntityNotFoundError<User>>> ApplyFormAsync(ApplyFormDto applyFormDto)
    {
        var (passport, visitTime, visitReason, userToVisitId, emailToSendReply) = applyFormDto;
        var findUserResult = await usersRepository.FindAsync(userToVisitId);
        if (findUserResult.TryGetFault(out var fault, out var user))
        {
            return fault;
        }

        var form = new Form(passport, visitTime, visitReason, user, emailToSendReply);
        await formsRepository.AddAsync(form);
        await visitRequestsRepository.AddAsync(new VisitRequest(form));
        return Result.Succeed();
    }

    public async Task<Result<IOperationError>> AcceptRequestAsync(Guid visitRequestId, Guid whoAcceptedId)
    {
        var findVisitRequestResult = await visitRequestsRepository.FindAsync(visitRequestId);
        if (findVisitRequestResult.TryGetFault(out var visitRequestNotFound, out var visitRequest))
        {
            return visitRequestNotFound;
        }

        var findUserResult = await usersRepository.FindAsync(whoAcceptedId);
        if (findUserResult.TryGetFault(out var userNotFound, out var user))
        {
            return userNotFound;
        }
        
        if (visitRequest.AcceptRequest(user).TryGetFault(out var acceptRequestError))
        {
            return acceptRequestError;
        }

        await visitRequestsRepository.UpdateAsync(visitRequest);

        await qrCodesService.CreateAndSendQrCodeAsync(visitRequest);
        
        return Result.Succeed();
    }
    
    public async Task<Result<IOperationError>> RejectRequestAsync(
        Guid visitRequestId, Guid whoProcessedId, string rejectionReason)
    {
        var findVisitRequestResult = await visitRequestsRepository.FindAsync(visitRequestId);
        if (findVisitRequestResult.TryGetFault(out var visitRequestNotFound, out var visitRequest))
        {
            return visitRequestNotFound;
        }
        
        var findUserResult = await usersRepository.FindAsync(whoProcessedId);
        if (findUserResult.TryGetFault(out var userNotFound, out var user))
        {
            return userNotFound;
        }

        if (visitRequest.RejectRequest(user, rejectionReason).TryGetFault(out var rejectRequestError))
        {
            return rejectRequestError;
        }

        await visitRequestsRepository.UpdateAsync(visitRequest);

        return Result.Succeed();
    }

    public async Task<IReadOnlyList<VisitRequest>> GetActiveVisitRequestsPage(int offset, int limit)
    {
        return await visitRequestsRepository.GetActiveVisitRequestsPageAsync(offset, limit);
    }

    public async Task<IReadOnlyList<VisitRequest>> GetUnactiveVisitRequestsPage(int offset, int limit)
    {
        return await visitRequestsRepository.GetUnactiveVisitRequestsPageAsync(offset, limit);
    }

    public async Task<IReadOnlyList<VisitRequest>> GetVisitRequestsPage(int offset, int limit)
    {
        return await visitRequestsRepository.GetVisitRequestsPageAsync(offset, limit);
    }

    public async Task<Result<EntityNotFoundError<User>, IReadOnlyList<VisitRequest>>> GetUserProcessedVisitRequestsPage(
        Guid userId, int offset, int limit)
    {
        var userExists = await usersRepository.ExistsAsync(userId);

        return userExists
            ? Result.Succeed(
                await visitRequestsRepository.GetUserProcessedVisitRequestsPageAsync(userId, offset, limit))
            : EntityNotFoundError<User>.FromId(userId);
    }

    public async Task<Result<EntityNotFoundError<VisitRequest>, VisitRequest>> GetVisitRequestById(Guid id)
    { 
        return await visitRequestsRepository.FindAsync(id);
    }
}