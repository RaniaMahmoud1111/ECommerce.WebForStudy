using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    // inner class (has field that has error )
    public class ValidationError
    {

        public  string Field  { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
