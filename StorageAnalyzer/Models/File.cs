using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageAnalyzer.Models
{
    public class File : Item
    {
        public File(string fullPath) : base(fullPath)
        {
            Size = GetSize();
        }

        public long GetSize()
        {
            return new FileInfo(FullPath).Length;
        }
    }
}
