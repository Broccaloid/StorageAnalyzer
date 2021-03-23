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
    public class ExpandableItemViewModel : BaseItemViewModel
    {
        private ICommand expandingCommand;
        private ObservableCollection<BaseItemViewModel> childrenItems;

        public ICommand ExpandingCommand
        {
            get
            {
                return expandingCommand ??= new RelayCommand((obj) => SetChildrenOnExpandAsync());
            }
        }

        public ObservableCollection<BaseItemViewModel> ChildrenItems
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

        public ExpandableItemViewModel(IItem item) : base(item)
        {
            ChildrenItems = new ObservableCollection<BaseItemViewModel>();
            ChildrenItems.Add(null);
        }

        public async Task SetChildrenOnExpandAsync()
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
                if (item as IExpandable != null)
                {
                    ChildrenItems.Add(new ExpandableItemViewModel(item));
                }
                else
                {
                    ChildrenItems.Add(new FileItemViewModel(item));
                }
            }
        }
    }
}
