using Examples.Notification.Api.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Infrastructure.Repositories
{
    public class BlockedTermsRepository : IBlockedTermsRepository
    {
        public async Task<IReadOnlyCollection<string>> GetAllAsync()
        {
            var blockedTerms = new string[]
            {
                "Bobo",
                "Chato",
                "Pateta",
                "Imbecil",
                "Fool",
                "Annoying",
                "Dumb",
                "Idiot"
            };

            return await Task.FromResult(blockedTerms);
        }
    }
}
