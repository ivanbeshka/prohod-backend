using Prohod.Domain.AggregationRoot;
using Prohod.Domain.Users;

namespace Prohod.Domain.Forms;

public class Form : IAggregationRoot
{
    public Guid Id { get; private set; }
    
    public Passport Passport { get; private set; }
    
    public DateTime VisitTime { get; private set; }
    
    public string VisitReason { get; private set; }
    
    public virtual User UserToVisit { get; private set; }
    
    public string EmailToSendReply { get; private set; }
    
#pragma warning disable CS8618
    protected Form() { }
#pragma warning restore CS8618

    public Form(Passport passport, DateTime visitTime, string visitReason, User userToVisit, string emailToSendReply)
    {
        Id = Guid.NewGuid();
        Passport = passport;
        VisitTime = visitTime;
        VisitReason = visitReason;
        UserToVisit = userToVisit;
        EmailToSendReply = emailToSendReply;
    }
}