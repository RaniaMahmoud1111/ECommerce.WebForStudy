using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    // here in that class i not put specific message , here we make the whole shap not specific message so we make that class abstract (cannot create obj from )
    public  abstract class NotFoundException(string message):Exception(message)
    {


    }
}
