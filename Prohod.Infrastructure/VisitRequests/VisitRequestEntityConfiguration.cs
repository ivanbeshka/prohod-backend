using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prohod.Domain.Forms;
using Prohod.Domain.VisitRequests;

namespace Prohod.Infrastructure.VisitRequests;

public class VisitRequestEntityConfiguration : IEntityTypeConfiguration<VisitRequest>
{
    private const string FormId = nameof(VisitRequest.Form) + nameof(Form.Id);

    public void Configure(EntityTypeBuilder<VisitRequest> builder)
    {
        builder.HasIndex(FormId).IsUnique();
    }
}