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
            try
            {
                foreach (var dir in drive.GetDirectories())
                {
                    childrenItems.Add(new Folder(dir.FullName));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                itemLogger.Error($"Failed to get access to drives content. Exception message: {e.Message}");
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
