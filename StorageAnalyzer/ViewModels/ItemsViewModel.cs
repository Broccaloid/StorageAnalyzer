using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace StorageAnalyzer
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items 
        { 
            get => items; 
            set 
            { 
                if(value == items)
                {
                    return;
                }
                items = value;
                OnPropertyChanged();
            } 
        }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Items.Add(new Drive(drive.Name));
                }
            }
        }

    }
}
