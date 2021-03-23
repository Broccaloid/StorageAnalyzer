using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Models
{
    public class Drive : ExpandableItem
    {
        public Drive(string fullPath) : base(fullPath)
        {
            Size = GetSize();
        }

        public override string Name => FullPath;

        public long GetSize()
        {
            var drive = new DriveInfo(FullPath);
            return drive.TotalSize - drive.AvailableFreeSpace;
        }
    }
}
