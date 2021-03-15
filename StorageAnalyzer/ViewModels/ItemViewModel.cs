using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace StorageAnalyzer.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private Item item;
        private ObservableCollection<ItemViewModel> childrenItems;
        private ICommand expandingCommand;

        public ICommand ExpandingCommand
        {
            get
            {
                return expandingCommand ??= new RelayCommand((obj) => SetChildrenOnExpand());
            }
        }


        public string Name
        {
            get => Item.Name;
        }

        public double Size
        {
            get => Item.SizeGb;
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

        public void SetChildrenOnExpand()
        {
            var expandableItem = Item as IExpandable;
            if (expandableItem == null || !ChildrenItems.Contains(null))
            {
                return;
            }
            ChildrenItems.Clear();

            foreach (var item in expandableItem.GetChildrenItems())
            {
                ChildrenItems.Add(new ItemViewModel(item));
            }
        }

        
    }
}
