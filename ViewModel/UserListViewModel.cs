using Lab04_CSharp.Exceptions;
using Lab04_CSharp.Models;
using Lab04_CSharp.Properties;
using Lab04_CSharp.Tools;
using Lab04_CSharp.Tools.Managers;
using Lab04_CSharp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Lab04_CSharp.ViewModel
{
    class UserListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _deleteCommand;

        public RelayCommand<object> AddCommand => _addCommand ?? (_addCommand = new RelayCommand<object>(AddImpl, o => true));
        public RelayCommand<object> EditCommand => _editCommand ?? (_editCommand = new RelayCommand<object>(EditImpl, o => true));
        public RelayCommand<object> DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(DeleteImpl, o => true));

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

        #region Commands

        private async void AddImpl(object o)
        {
            var done = await Task.Run(() =>
            {
                try
                {
                    StationManager.DataStorage.AddUser(new Person(_name, _surname, _email, _birthDate));
                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
                    MessageBox.Show("Person was successfully added");
                }
                catch (InvalidFormatEmailException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Invalid value: {ex.Email}");
                }
                catch (BirthdateInFutureException ex1)
                {
                    Console.WriteLine($"Error: {ex1.Message}");
                    Console.WriteLine($"Invalid value: {ex1.Age}");
                }
                catch (BirthdateInPastException ex2)
                {
                    Console.WriteLine($"Error: {ex2.Message}");
                    Console.WriteLine($"Invalid value: {ex2.Age}");
                }

                return true;
            });

            if (done)
            {
                Name = "";
                Surname = "";
                Email = "";
                Birthday = DateTime.Today;
            }
        }


        private async void EditImpl(object o)
        {
            await Task.Run(() =>
            {
                try
                {
                    Name = SelectedItem.Name;
                    Surname = SelectedItem.Surname;
                    Email = SelectedItem.Email;
                    Birthday = SelectedItem.Birthday;
                    DeleteImpl(SelectedItem);

                }
                catch (Exception)
                {
                    MessageBox.Show("Some trouble with edit");
                }
            });
        }


        private async void DeleteImpl(object o)
        {
            await Task.Run(() =>
            {
                try
                {
                    StationManager.DataStorage.DeleteUser(SelectedItem);

                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);

                    MessageBox.Show("Person was successfully deleted");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Some trouble with delete");
                }
            });
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
