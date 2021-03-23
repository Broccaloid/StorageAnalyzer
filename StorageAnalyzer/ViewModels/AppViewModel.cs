using NLog;
using StorageAnalyzer.Models;
using StorageAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace StorageAnalyzer
{
    public class AppViewModel : BaseViewModel
    {
        private ObservableCollection<ExpandableItemViewModel> items;
        private static Logger appViewModelLogger = LogManager.GetLogger("LoggerRules");

        public ObservableCollection<ExpandableItemViewModel> Items
        {
            get => items;
            set
            {
                if (value == items)
                {
                    return;
                }
                items = value;
                OnPropertyChanged();
            }
        }

        public AppViewModel()
        {
            Items = new ObservableCollection<ExpandableItemViewModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Items.Add(new ExpandableItemViewModel(new Drive(drive.Name)));
                }
            }
            appViewModelLogger.Info("App was started successfully");
        }

    }
}
