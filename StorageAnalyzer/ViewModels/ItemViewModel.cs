using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StorageAnalyzer.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private Item item;
        private ObservableCollection<ItemViewModel> childrenItems;
        private RelayCommand expandCommand;

        public string Name
        {
            get => Item.Name;
        }

        public long Size
        {
            get => Item.Size;
        }
        public RelayCommand ExpandCommand
        {
            get
            {
                return expandCommand ??= new RelayCommand(obj =>
                {
                    var item = obj as IExpandable;
                    if (item == null)
                    {
                        return;
                    }
                    ChildrenItems.Clear();
                    var childrenItems = item.GetChildrenItems();
                    foreach (var child in childrenItems)
                    {
                        ChildrenItems.Add(new ItemViewModel(child));
                    }
                });
            }
        }

        public ObservableCollection<ItemViewModel> ChildrenItems
        {
            get => childrenItems;
            set
            {
                if (value == childrenItems)
                {
                    return;
                }
                childrenItems = value;
                OnPropertyChanged();
            }
        }

        public Item Item
        {
            get => item;
            set
            {
                if (Item == value)
                {
                    return;
                }
                item = value;
                OnPropertyChanged();
            }
        }

        public ItemViewModel(Item item)
        {
            Item = item;
            ChildrenItems = new ObservableCollection<ItemViewModel>();
            if (item as IExpandable != null)
            {
                ChildrenItems.Add(null);
            }
        }



        
    }
}
