using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StorageAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StorageAnalyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ThreadPool.SetMaxThreads(10, 10);
            MainWindow = new MainWindow()
            {
                DataContext = new AppViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}





