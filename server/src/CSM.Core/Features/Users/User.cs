using CSM.Core.Common;
using CSM.Core.Features.Channels;

namespace CSM.Core.Features.Users;

public sealed class User : IAuditableEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string NickName { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;
    
    public string? MiddleName { get; private set; }
    public string? LastName { get; private set; }

    /// <summary>
    /// Type of gender
    /// </summary>
    public GenderType GenderType { get; private set; } = GenderType.M;
    
    /// <summary>
    /// ID of country
    /// </summary>
    public Guid CountryId { get; private set; }

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
    
    public static User CreateUser(string nickName, string firstName, string? middleName, string? lastName, 
        GenderType genderType, Guid countryId, string timeZone, string locale)
    {
        return new User
        {
            NickName = nickName,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            GenderType = genderType,
            CountryId = countryId,
            TimeZone = timeZone,
            Locale = locale,
        };
    }
}