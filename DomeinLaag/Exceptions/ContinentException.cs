using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Exceptions
{

    public class ContinentException : DomainException

    {
        public ContinentException(string message) : base(message)
        {

        }
    }
}
