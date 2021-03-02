using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    }
}
