using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageAnalyzer.Models
{
    public class Folder : Item
    {
        public Folder(string fullPath) : base(fullPath) { }
        public override long GetSize()
        {
            var directory = new DirectoryInfo(FullPath);
            long size = 0;
            foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                size += file.Length;
            }
            return size;
        }
    }
}
