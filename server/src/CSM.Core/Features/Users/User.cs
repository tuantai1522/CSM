using CSM.Core.Common;
using CSM.Core.Features.Channels;

namespace CSM.Core.Features.Users;

public sealed class User : IAuditableEntity, IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string NickName { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;
    
    public string? MiddleName { get; private set; }
    public string? LastName { get; private set; }

    /// <summary>
    /// Information for login
    /// </summary>
    public string Email { get; private set; } = null!;
    public string HashPassword { get; private set; } = null!;

    /// <summary>
    /// Type of gender
    /// </summary>
    public GenderType GenderType { get; private set; } = GenderType.M;
    
    /// <summary>
    /// ID of city
    /// </summary>
    public Guid CityId { get; private set; }

    /// <summary>
    /// Information from header
    /// </summary>
    public string TimeZone { get; private set; } = null!;
    public string Locale { get; private set; } = null!;
    
    public long CreatedAt { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public long? UpdatedAt { get; private set; }

    public long? DeletedAt { get; private set; }

    /// <summary>
    /// List channels of this user.
    /// </summary>
    public IReadOnlyList<ChannelMember> ChannelMembers { get; init; } = null!;

    private User()
    {
        
    }
    
    public static User CreateUser(string nickName, string firstName, string? middleName, string? lastName, string email, string hashPassword,
        GenderType genderType, Guid cityId, string timeZone, string locale)
    {
        return new User
        {
            NickName = nickName,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            Email = email,
            HashPassword = hashPassword,
            GenderType = genderType,
            CityId = cityId,
            TimeZone = timeZone,
            Locale = locale,
        };
    }
}