

using Microsoft.Extensions.Logging;
using NLog;
using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageAnalyzer.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private Item item;
        private ObservableCollection<ItemViewModel> childrenItems;
        private ICommand expandingCommand;
        private static Logger itemViewModelLogger = LogManager.GetLogger("LoggerRules");

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

        public async Task SetChildrenOnExpand()
        {
            var expandableItem = Item as IExpandable;
            if (expandableItem == null || !ChildrenItems.Contains(null))
            {
                return;
            }
            itemViewModelLogger.Info($"Node {Name} was expanded");
            ChildrenItems.Clear();

            foreach (var item in await Task.Run(() => expandableItem.GetChildrenItems()))
            {
                ChildrenItems.Add(new ItemViewModel(item));
            }
        }

        
    }
}
