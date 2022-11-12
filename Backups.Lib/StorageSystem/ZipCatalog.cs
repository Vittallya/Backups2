using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    class ZipCatalog : IZipObject
    {
        private readonly List<IZipObject> childs;
        public string Name { get; }

        public bool IsCatalog => true;

        public ZipCatalog(string name, List<IZipObject> childsList)
        {
            Name = name;
            childs = childsList;
        }

        public void AddObject(IZipObject obj)
        {
            childs.Add(obj);
        }

        public void RemoveObject(IZipObject obj)
        {
            childs.Remove(obj);
        }

        public override bool Equals(object obj)
        {
            if (obj is IZipObject zip) return zip.Name == Name && zip.IsCatalog;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool HasZipObject(IZipObject obj)
        {
            return childs.Any(x => x.Name == obj.Name && x.IsCatalog == obj.IsCatalog);
        }
    }
}
