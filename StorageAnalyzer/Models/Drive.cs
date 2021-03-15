using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace StorageAnalyzer.Models
{
    public class Drive : Item, IExpandable
    {
        public Drive(string fullPath) : base(fullPath)
        {
            Size = GetSize();
        }

        public override string Name => FullPath;
        public List<Item> GetChildrenItems()
        {
            var childrenItems = new List<Item>();
            var drive = new DirectoryInfo(FullPath);
            foreach (var dir in drive.GetDirectories())
            {
                childrenItems.Add(new Folder(dir.FullName));
            }
            return childrenItems;
        }

        public long GetSize()
        {
            var drive = new DriveInfo(FullPath);
            return drive.TotalSize - drive.AvailableFreeSpace;
        }
    }
}
