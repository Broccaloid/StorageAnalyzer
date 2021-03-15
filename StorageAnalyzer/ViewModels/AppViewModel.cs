using StorageAnalyzer.Models;
using StorageAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace StorageAnalyzer
{
    public class AppViewModel : BaseViewModel
    {
        private ObservableCollection<ItemViewModel> items;
        private RelayCommand expandCommand;

        public RelayCommand ExpandCommand
        {
            get
            {
                return expandCommand ??= new RelayCommand(obj =>
                {
                    foreach (var item in Items)
                    {
                        item.SetChildrenOnExpand();
                    }
                });
            }
        }

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
