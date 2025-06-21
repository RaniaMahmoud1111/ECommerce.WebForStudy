
using Domain.Execptions;

namespace Services
{
    
    public sealed class DeliveryMethodNotFoundException(int id ) : NotFoundException($"No DeliveryMethod of id ={id} Found !")
    {
         


    }
}