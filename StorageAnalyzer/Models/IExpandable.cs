using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Models
{
    public interface IExpandable : IItem
    {
        public List<IItem> GetChildrenItems();
    }
}
