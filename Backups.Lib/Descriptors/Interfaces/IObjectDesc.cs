using Backups.Lib.Visitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.Descriptors
{
    public interface IObjectDesc: IEquatable<IObjectDesc>
    {
        ICatalogDesc MainCatalog { get; }
        ICatalogDesc ParentCatalog { get; }
        //Абсолютный путь
        string PathGlobal { get; }

        //Относительный путь
        string PathLocal { get; }

        string Name { get; }

        string PathWithoutName { get; }

        void Accept(IVisitor visitor);

        bool IEquatable<IObjectDesc>.Equals(IObjectDesc obj)
        {
            return this.PathGlobal.Equals(obj.PathGlobal);
        }
    }
}
