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
    
    ChannelIdEmpty = 1013,
    ChannelNameEmpty = 1014,
    ListMembersAndOwnersEmpty = 1015,
    ThisUserIsNotInChannel = 1016,
    UnAuthorizedInChannel = 1017,
    MessageEmpty = 1018,
    ThisRootIdNotInChannel = 1019,
    RootMessageIsRootOfAnotherRoot = 1020,
    ThisUserIsAlreadyInChannel = 1021,
    PageMustBePositive = 1022,
    PageSizeMustBePositive = 1023,
    
    ThisUserAlreadyBelongsToThisRole = 1024,
    RoleIdEmpty = 1025,
    InvalidPermissionsValue = 1026,
    CanNotAssignToParentView = 1027,
    ListViewEmpty = 1028,
}