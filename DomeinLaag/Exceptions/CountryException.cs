using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Exceptions
{
    public class CountryException : DomainException
    {
        public CountryException(string message) : base(message)
        {

        }
    }
}
