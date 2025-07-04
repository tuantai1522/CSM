using CSM.Core.Common;

namespace CSM.Core.Features.Users;

public static class UserErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
}
