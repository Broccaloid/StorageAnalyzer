using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace StorageAnalyzer.Models
{
    public class Folder : Item, IExpandable
    {
        public Folder(string fullPath) : base(fullPath)
        {

        }
        public override long GetSize()
        {
            //var directory = new DirectoryInfo(FullPath);
            long size = 0;
            //foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
            foreach (var file in GetAllFiles(FullPath, "*"))
            {
                size += file.Length;
            }
            return size;
        }

        public void SetChildrenItems()
        {
            ChildrenItems.Clear();

            try
            {
                var drive = new DirectoryInfo(FullPath);
                foreach (var dir in drive.GetDirectories())
                {
                    ChildrenItems.Add(new Folder(dir.FullName));
                }
                foreach (var file in drive.GetFiles())
                {
                    ChildrenItems.Add(new File(file.FullName));
                }
            }
            catch (UnauthorizedAccessException e)
            {

            };
        }

        private IEnumerable<String> GetAllFiles(string path, string searchPattern)
        {
            return System.IO.Directory.EnumerateFiles(path, searchPattern).Union(
            System.IO.Directory.EnumerateDirectories(path).SelectMany(d =>
            {
                try
                {
                    return GetAllFiles(d, searchPattern);
                }
                catch (UnauthorizedAccessException e)
                {
                    return Enumerable.Empty<String>();
                }
            }));
        }
    }
}
