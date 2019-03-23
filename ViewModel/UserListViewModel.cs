using Lab04_CSharp.Models;
using Lab04_CSharp.Properties;
using Lab04_CSharp.Tools;
using Lab04_CSharp.Tools.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lab04_CSharp.ViewModel
{
    class UserListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;
        #endregion

        public Person SelectedItem { get; set; }
        private ObservableCollection<Person> _persons;

        #region Properties
        public DateTime Birthday
        {
            get { return _birthDate; }
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        #endregion
        
        public ObservableCollection<Person> Persons
        {
            get => _persons;

            private set
            { 
                _persons = value;
                OnPropertyChanged();
            }
        }

        internal UserListViewModel()
        {
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }




        public event PropertyChangedEventHandler PropertyChanged;
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
