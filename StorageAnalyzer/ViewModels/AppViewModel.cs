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
        private ObservableCollection<ItemViewModel> items;

        public ObservableCollection<ItemViewModel> Items
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
            Items = new ObservableCollection<ItemViewModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Items.Add(new ItemViewModel(new Drive(drive.Name)));
                }
            }
        }

    }
}
