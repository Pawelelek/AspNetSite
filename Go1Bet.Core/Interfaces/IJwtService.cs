using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Entities.Tokens;

namespace Go1Bet.Core.Interfaces
{
    public interface IJwtService
    {
        Task Create(RefreshToken token);
        Task Delete(RefreshToken token);
        Task Update(RefreshToken token);
        Task<RefreshToken?> Get(string token);
        Task<IEnumerable<RefreshToken>> GetAll();
    }
}
