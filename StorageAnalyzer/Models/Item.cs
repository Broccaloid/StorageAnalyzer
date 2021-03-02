using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StorageAnalyzer
{
    public abstract class Item : INotifyPropertyChanged
    {
        private string fullPath;
        private ObservableCollection<Item> childrenItems;
        private int size;

        public string FullPath
        {
            get
            {
                return fullPath;
            }
            set
            {
                if(fullPath == value)
                {
                    return;
                }

                fullPath = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                var normalizedPath = FullPath.Replace('/', '\\');

                var lastIndex = normalizedPath.LastIndexOf('\\');

                if (lastIndex <= 0)
                    return FullPath;

                return FullPath.Substring(lastIndex + 1);
            }
        }

        public ObservableCollection<Item> ChildrenItems 
        { 
            get => childrenItems;
            set
            {
                if (childrenItems == value)
                {
                    return;
                }

                childrenItems = value;
                OnPropertyChanged();
            }
        }

        public int Size 
        { 
            get => size;
            set
            {
                if (size == value)
                {
                    return;
                }
                size = value;
                OnPropertyChanged();
            }
        }

        public Item(string fullPath)
        {
            FullPath = fullPath;
            Size = SetSize();
        }

        public abstract int SetSize();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
