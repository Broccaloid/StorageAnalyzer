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
                if (value == items)
                {
                    return;
                }
                items = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand getChildrenOnExpandCommand;
        public RelayCommand GetChildrenOnExpandCommand
        {
            get
            {
                return getChildrenOnExpandCommand ??= new RelayCommand(obj =>
                    {
                        var item = obj as IExpandable;
                        if (item == null)
                        {
                            return;
                        }
                        item.SetChildrenItems();
                    });
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
