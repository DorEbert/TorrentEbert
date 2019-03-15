using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class IllegalException : Exception
    {
        public IllegalException() : base("There was an error while preforming the action. The data you have entered may be wrong.")
        { }
    }
}
