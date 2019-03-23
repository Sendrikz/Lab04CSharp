using Lab04_CSharp.Models;
using Lab04_CSharp.Tools.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_CSharp.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _users;
        
        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = PersonGenerator.getGeneratedPersons(50);

                SaveChanges();
            }

        }

        public List<Person> AddUser(Person user)
        { 
            _users.Add(user);
            SaveChanges();

            return _users;
        }



        public List<Person> DeleteUser(Person user)
        {
            _users.Remove(user);
            SaveChanges();

            return _users;
        }
        
        public List<Person> UsersList
        {
            get { return _users.ToList(); }
        }
        
        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
    }
}
