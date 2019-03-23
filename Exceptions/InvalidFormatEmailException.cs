using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_CSharp.Exceptions
{
    class InvalidFormatEmailException : FormatException
    {
        public string Email { get; }
        public InvalidFormatEmailException(string message, string email)
        : base(message)
        {
            Email = email;
        }
    }
}
