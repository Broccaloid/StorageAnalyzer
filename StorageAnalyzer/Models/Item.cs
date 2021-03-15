using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace StorageAnalyzer
{
    public abstract class Item : INotifyPropertyChanged
    {
        private string fullPath;
        private long size;

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

        public virtual string Name
        {
            get
            {
                var normalizedPath = FullPath.Replace('/', '\\');
                var lastIndex = normalizedPath.LastIndexOf('\\');
                return FullPath.Substring(lastIndex + 1);
            }
        }

        public double SizeGb
        {
            get => Math.Round(Size / 1073741824.00, 2);
        }
        public long Size 
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
