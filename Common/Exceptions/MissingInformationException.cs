using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class MissingInformationException : Exception
    {
        public MissingInformationException(string missingArg) : base ($"Miising arguments. {missingArg} is required!")
        { }
    }
}
