using Prohod.Domain.Forms;
using Prohod.Infrastructure.Database;

namespace Prohod.Infrastructure.Forms;

public class FormsRepository : RepositoryBase<Form>, IFormsRepository
{
    public FormsRepository(IAppDbContext dbContext) : base(dbContext)
    {
    }
}