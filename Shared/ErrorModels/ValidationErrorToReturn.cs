using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorToReturn // i will applay it if the error happen instead of normal behaviour of api  that make in program.cs
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest; // needt cansting as it enum 

        public string Message { get; set; } = "Validation Failed ! ";
        public IEnumerable<ValidationError> ValidationErrors { get; set; }


    }
}
