namespace WebServerV._2.Server.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) 
            : base(message)
        {
        }
    }
}
