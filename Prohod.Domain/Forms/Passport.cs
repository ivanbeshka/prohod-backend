namespace Prohod.Domain.Forms;

public record Passport(
    string FullName,
    string Series,
    string Number,
    string WhoIssued,
    DateTime IssueDate);