using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageAnalyzer.Models
{
    public abstract class ExpandableItem : BaseItem, IExpandable
    {
        protected ExpandableItem(string fullPath) : base(fullPath)
        {
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
