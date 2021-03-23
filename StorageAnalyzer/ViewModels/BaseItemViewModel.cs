using NLog;
using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageAnalyzer.ViewModels
{
    public abstract class BaseItemViewModel : BaseViewModel
    {
        protected static Logger itemViewModelLogger = LogManager.GetLogger("LoggerRules");
        private IItem item;

        public BaseItemViewModel(IItem item)
        {
            Item = item;
        }

        public IItem Item
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
        public string Name
        {
            get => Item.Name;
        }

        public string Size
        {
            get
            {
                var sizeGb = Math.Round(Item.Size / 1073741824.00, 2);
                if (sizeGb != 0)
                {
                    return $" {sizeGb} Gb";
                }
                return $" {Math.Round(Item.Size / 1048576.00, 2)} Mb";
            }
        }
    }
}
