using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Exceptions
{
    public class CityException : Exception
    {
        public CityException(string message) : base(message)
        {

        }
    }
}
