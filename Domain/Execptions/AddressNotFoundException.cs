
using Domain.Execptions;

namespace Services
{
   
    public  sealed class AddressNotFoundException(string userName) :NotFoundException($"User {userName} has no Address")
    {
       


    }
}