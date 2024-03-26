namespace Prohod.Domain.Forms;

public record ApplyFormDto(
    Passport Passport,
    DateTime VisitTime,
    string VisitReason,
    Guid UserToVisitId,
    string EmailToSendReply);