using NLog;
using StorageAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace StorageAnalyzer
{
    public class BaseItem : INotifyPropertyChanged, IItem
    {
        private string fullPath;
        protected long size;
        protected static Logger itemLogger = LogManager.GetLogger("LoggerRules");

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

        public double SizeMb
        {
            get => Math.Round(Size / 1048576.00, 2);
        }
        public virtual long Size 
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

        public BaseItem(string fullPath)
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
