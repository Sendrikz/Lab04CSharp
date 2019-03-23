using Lab04_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_CSharp.Tools.DataStorage
{
    internal interface IDataStorage
    {
        List<Person> AddUser(Person user);

        List<Person> DeleteUser(Person user);

        List<Person> UsersList { get; }
    }
}
