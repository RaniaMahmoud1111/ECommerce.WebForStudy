using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    // here we pass the specific message , that class sealed to make it cannot be inhereted 
    public sealed class ProductNotFoundException(int id):NotFoundException($"Product with id={id} is not found")
    {

    }
}
