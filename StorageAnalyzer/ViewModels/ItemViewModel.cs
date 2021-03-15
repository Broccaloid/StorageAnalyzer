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
        private bool isExpanded;
        

        public string Name
        {
            get => Item.Name;
        }

        public long Size
        {
            get => Item.Size;
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (value == isExpanded)
                {
                    return;
                }
                isExpanded = value;
                if (value == true)
                {
                    SetChildrenOnExpand();
                }
                else
                {
                    ChildrenItems.Clear();
                    ChildrenItems.Add(null);
                }
                OnPropertyChanged();
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

        public void SetChildrenOnExpand()
        {
            var expandableItem = Item as IExpandable;
            if (expandableItem == null || IsExpanded == false)
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
