using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class ErrorMessageRepository(ApplicationDbContext context) : IErrorMessageRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<ErrorMessage> GetErrorMessageByErrorCodeAndLanguageTypeAsync(ErrorCode errorCode,
        LanguageType languageType, CancellationToken cancellationToken)
        => (await context.ErrorMessages.FirstOrDefaultAsync(x => x.LanguageType == languageType &&
                                                                 x.ErrorCode == errorCode,
            cancellationToken))!;

    public async Task<IReadOnlyList<ErrorMessage>> GetErrorMessageByErrorCodesAndLanguageTypeAsync(
        List<ErrorCode> errorCodes,
        LanguageType languageType, CancellationToken cancellationToken)
        => await context.ErrorMessages
            .Where(x => x.LanguageType == languageType && errorCodes.Contains(x.ErrorCode))
            .ToListAsync(cancellationToken);
}