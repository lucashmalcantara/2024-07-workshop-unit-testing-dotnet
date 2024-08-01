using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Domain.Repositories
{
    public interface IBlockedTermsRepository
    {
        public Task<IReadOnlyCollection<string>> GetAllAsync();
    }
}
