using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    public interface IZipObject
    {
        public bool IsCatalog { get; }
        public string Name { get; }
    }
}
