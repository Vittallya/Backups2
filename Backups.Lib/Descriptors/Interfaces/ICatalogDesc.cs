using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.Descriptors
{
    public interface ICatalogDesc: IObjectDesc
    {
        IReadOnlyCollection<IObjectDesc> Childs { get; }
        IReadOnlyCollection<IObjectDesc> AllChilds { get; }
        public bool IsMainCatalog { get; }
        IReadOnlyCollection<ICatalogDesc> SubCatalogs { get; }
        IReadOnlyCollection<IFileDesc> SubFiles { get; }

        internal void AddObject(IObjectDesc desc);
        internal void RemoveObject(IObjectDesc desc);
    }
}
