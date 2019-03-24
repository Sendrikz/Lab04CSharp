using Lab04_CSharp.Models;
using System.Collections.Generic;

namespace Lab04_CSharp.Tools.DataStorage
{
    internal interface IDataStorage
    {
        List<Person> AddUser(Person user);

        List<Person> DeleteUser(Person user);

        List<Person> UsersList { get; }
    }
}
