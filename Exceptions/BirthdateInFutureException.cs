using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_CSharp.Exceptions
{
    class BirthdateInFutureException : ArgumentException
    {
        public int Age { get; }
        public BirthdateInFutureException(string message, int age)
        : base(message)
        {
            Age = age;
        }
    }
}
