using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Models
{
    public class Folder : BaseItem, IExpandable
    {
        public Folder(string fullPath) : base(fullPath)
        {
            Size = GetSizeAsync(fullPath).Result;
        }

        private async Task<long> GetSizeAsync(string path)
        {
            var tasks = new List<Task<long>>();
            var directory = new DirectoryInfo(path);
            long folderSize = 0;
            try
            {
                foreach (var file in directory.GetFiles())
                {
                    folderSize += file.Length;
                }

                foreach (var dir in directory.GetDirectories())
                {
                    tasks.Add(Task.Run(() => GetSizeAsync(dir.FullName)));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
            }
            var results = await Task.WhenAll(tasks);
            foreach (var item in results)
            {
                folderSize += item;
            }
            return folderSize;
        }

        public List<IItem> GetChildrenItems()
        {
            var childrenItems = new List<IItem>();
            try
            {
                var drive = new DirectoryInfo(FullPath);
                foreach (var dir in drive.GetDirectories())
                {
                    childrenItems.Add(new Folder(dir.FullName));
                }
                foreach (var file in drive.GetFiles())
                {
                    childrenItems.Add(new File(file.FullName));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
            };
            return childrenItems;
        }
    }
}
