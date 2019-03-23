using Lab04_CSharp.Tools.DataStorage;
using Lab04_CSharp.Tools.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab04_CSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()

        {

            InitializeApplication();

            InitializeComponent();



        }



        private void InitializeApplication()

        {

            StationManager.Initialize(new SerializedDataStorage());

        }
    }
}
