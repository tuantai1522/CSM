using CSM.Core.Common;

namespace CSM.Core.Features.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedEmailAsync(string email, CancellationToken cancellationToken);
}