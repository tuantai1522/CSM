using CSM.Core.Common;
using CSM.Core.Features.Users;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AddAsync(user, cancellationToken);
        
        return result.Entity;
    }

    public async Task<bool> VerifyExistedEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(x => x.Email == email, cancellationToken);
    }
}