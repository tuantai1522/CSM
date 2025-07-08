namespace CSM.Core.Features.ErrorMessages;

public enum ErrorCode
{
    NotFoundById = 1000,
    EmailNotUnique = 1001,
    NotFoundByEmail = 1002,
    NotFoundByCityInCountry = 1003,
    CityNameEmpty = 1004,
    CountryNameEmpty = 1005,
    CountryIdEmpty = 1006,
    CityIdEmpty = 1007,
    EmailEmpty = 1008,
    InvalidEmail = 1009,
    PasswordEmpty = 1010,
    NickNameEmpty = 1011,
    FirstNameEmpty = 1012,
}