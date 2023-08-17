namespace Kusys.Core.Messages;

public enum ErrorMessageCode
{
    UsernameAlreadyExists = 101,
    TrIdentityNumberAlreadyExists = 201,
    EmailAlreadyExists = 102,
    CheckYourEmail = 153,
    UserNotFound = 156,
    StudentNotFound = 256,
    UserCouldNotRemove = 158,
    StudentCouldNotRemove = 258,
    UserCouldNotFind = 159,
    StudentCouldNotFind = 259,
    UserCouldNotInserted = 160,
    StudentCouldNotInserted = 260,
    UserCouldNotUpdated = 161,
    StudentCouldNotUpdated = 261,
    UserIsDelete = 162,
}
