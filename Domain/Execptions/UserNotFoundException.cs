
namespace Services
{

    public sealed class UserNotFoundException(string email) : Exception($"User with email= {email} is not found!")
    {

       

    }
}