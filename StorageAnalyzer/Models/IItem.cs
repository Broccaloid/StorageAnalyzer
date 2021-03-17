using System;
using System.Collections.Generic;
using System.Text;

namespace StorageAnalyzer.Models
{
    public interface IItem
    {
        public string Name { get;}
        public double SizeGb { get;}
        public double SizeMb { get;}
    }
}
