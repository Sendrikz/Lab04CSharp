using Lab04_CSharp.Exceptions;
using Lab04_CSharp.Models;
using Lab04_CSharp.Properties;
using Lab04_CSharp.Tools;
using Lab04_CSharp.Tools.Managers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Lab04_CSharp.ViewModel
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;
        private bool _sortingAsc = true;
        private string _filterQuery;
        private Boolean _showCancelFilter;

        public Person SelectedItem { get; set; }
        public string SelectedSortFilterProperty { get; set; }
        private ObservableCollection<Person> _persons;
        private ObservableCollection<Person> _supportPersonsList;
        private static CollectionView _sortFilterOptionsCollection;

        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _filterCommand;
        private RelayCommand<object> _cancelCommand;

        public RelayCommand<object> AddCommand => _addCommand ?? (_addCommand = new RelayCommand<object>(AddImpl, o => true));
        public RelayCommand<object> EditCommand => _editCommand ?? (_editCommand = new RelayCommand<object>(EditImpl, o => true));
        public RelayCommand<object> DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(DeleteImpl, o => true));
        public RelayCommand<object> FilterCommand => _filterCommand ?? (_filterCommand = new RelayCommand<object>(FilterImpl, o => true));
        public RelayCommand<object> CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(CancelImpl, o => true));

        #endregion

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

        public string FilterQuery
        {
            get { return _filterQuery; }
            set { _filterQuery= value; OnPropertyChanged(); }
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }

            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> SupportPersonsList
        {
            get { return _supportPersonsList; }

            private set
            {
                _supportPersonsList = value;
            }
        }

        public Boolean ShowCancelFilter
        {
            get { return _showCancelFilter; }

            private set
            {
                _showCancelFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion


        internal UserListViewModel()
        {
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }

        #region Commands

        private async void AddImpl(object o)
        {
            var complete = await Task.Run(() =>
            {
                try
                {
                    StationManager.DataStorage.AddUser(new Person(Name, Surname, Email, Birthday));
                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
                    MessageBox.Show("Person was successfully added");
                }
                catch (InvalidFormatEmailException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Invalid value: {ex.Email}");
                    MessageBox.Show("Wrong email format!");
                }
                catch (BirthdateInFutureException ex1)
                {
                    Console.WriteLine($"Error: {ex1.Message}");
                    Console.WriteLine($"Invalid value: {ex1.Age}");
                    MessageBox.Show("Wrong birthday date!");
                }
                catch (BirthdateInPastException ex2)
                {
                    Console.WriteLine($"Error: {ex2.Message}");
                    Console.WriteLine($"Invalid value: {ex2.Age}");
                    MessageBox.Show("Wrong birthday date!");
                }

                return true;
            });

            if (complete)
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
                    MessageBox.Show("The proccess of editing person finished with exception");
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
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("The proccess of deleting person finished with exception");
                }
            });
        }

        public static CollectionView SortFilterOptions => _sortFilterOptionsCollection ??
                                                          (_sortFilterOptionsCollection =
                                                              new CollectionView(SortExtension.SortFiltertOptions));

        public RelayCommand<object> SortCommand =>
           _sortCommand ?? (_sortCommand =
               new RelayCommand<object>(SortImpl, o => !string.IsNullOrEmpty(SelectedSortFilterProperty)));

        private async void SortImpl(object o)
        {
            await Task.Run(() =>
            {
                Persons = new ObservableCollection<Person>(SortExtension.SortByProperty(_persons.ToList(), SelectedSortFilterProperty, _sortingAsc));
                _sortingAsc = !_sortingAsc;
            });
        }

        public async void FilterImpl(object o)
        {
            await Task.Run(() =>
            {
                SupportPersonsList = Persons;
                Persons = new ObservableCollection<Person>(SortExtension.FilterByProperty(_persons.ToList(), SelectedSortFilterProperty, FilterQuery));

                ShowCancelFilter = true;
            });
        }

        public async void CancelImpl(object o)
        {
            await Task.Run(() =>
            {
                Persons = SupportPersonsList;
                ShowCancelFilter = false;
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
