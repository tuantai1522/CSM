using CSM.Core.Common;

namespace CSM.Core.Features.Countries;

public static class CountryErrors
{
    public static readonly Error NotFoundById = Error.NotFound(
        "Users.NotFoundById",
        "The user with the specified id was not found");
    
    public static readonly Error CityAlreadyExistedInCountry = Error.Validation(
        "Users.CityAlreadyExistedInCountry",
        "This city already exists in the country");
    
    public static readonly Error CityNotFoundInCountry = Error.Validation(
        "Users.CityNotFoundInCountry",
        "This city is not found in the country");
}
