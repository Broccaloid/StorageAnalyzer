using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageAnalyzer.Models
{
    public class Drive : Item
    {
        public Drive(string fullPath) : base(fullPath) { }
        public override long GetSize()
        {
            var drive = new DriveInfo(FullPath);
            return drive.TotalSize - drive.AvailableFreeSpace;
        }
    }
}
