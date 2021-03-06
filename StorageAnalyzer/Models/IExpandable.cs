using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StorageAnalyzer.Models
{
    interface IExpandable
    {
        public void SetChildrenItems();
    }
}
