using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Plarium
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void OnStartup(Object sender, StartupEventArgs e)
        {
            Views.DirectoryView view = new Views.DirectoryView();
            view.DataContext = new ViewModels.DirectoryViewModel();
            view.Show();
        }
    }

}
