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
            SetChildrenItems();
        }

        public void SetChildrenItems()
        {
            ChildrenItems.Clear();
            var drive = new DirectoryInfo(FullPath);
            foreach (var dir in drive.GetDirectories())
            {
                ChildrenItems.Add(new Folder(dir.FullName));
            }
        }

        public long GetSize()
        {
            var drive = new DriveInfo(FullPath);
            return drive.TotalSize - drive.AvailableFreeSpace;
        }
    }
}
