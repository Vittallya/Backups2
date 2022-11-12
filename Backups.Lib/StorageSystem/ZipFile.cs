using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    class ZipFile : IZipObject
    {
        public string Name { get; }

        public bool IsCatalog => false;

        public ZipFile(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is IZipObject zipObj) return zipObj.Name == Name && !IsCatalog;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
