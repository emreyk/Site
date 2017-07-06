

namespace Proje.Entities.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExists=101, //kullanıcı var
        EmailAlreadyExists=102,    //email var
        UserIsNotActive=103,
             
        UsernameOrPassWrong=152,
        CheckYourEmail=153,
        UserNotFound=154,
        ProfileCouldNotUpdated=155

    }
}
