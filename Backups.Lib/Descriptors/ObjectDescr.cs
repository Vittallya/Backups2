using Backups.Lib.Visitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Descriptors
{
    abstract class ObjectDescr : IObjectDesc
    {
        public ICatalogDesc MainCatalog { get; }

        public ICatalogDesc ParentCatalog { get; }

        public string PathGlobal { get; }

        public string RelativePath { get; }

        public string Name { get; }

        public string PathWithoutName { get; }

        public abstract void Accept(IVisitor visitor);

        public ObjectDescr(ICatalogDesc parent, string name):
            this(Path.Combine(parent.PathGlobal, name))

        {
            ParentCatalog = parent ?? throw new ArgumentException("parent object is null");

            var mainCatalog = parent;

            while (!mainCatalog.IsMainCatalog)
                mainCatalog = mainCatalog.ParentCatalog;

            MainCatalog = mainCatalog;
            RelativePath = PathGlobal.Substring((mainCatalog.PathGlobal.Length + 1));
        }


        public ObjectDescr(string globalPath)
        {
            PathGlobal = globalPath;

            if (globalPath.Contains('\\'))
            {
                Name = globalPath.Substring(globalPath.LastIndexOf('\\') + 1);
                PathWithoutName = globalPath.Substring(0, globalPath.LastIndexOf('\\'));
            }
            else
            {
                Name = globalPath;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is IObjectDesc d)
                return PathGlobal.Equals(d.PathGlobal);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return PathGlobal.GetHashCode();
        }
    }
}
