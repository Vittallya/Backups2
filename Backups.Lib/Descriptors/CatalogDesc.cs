using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.Descriptors
{
    class CatalogDescriptor : ObjectDescr, ICatalogDesc
    {
        private readonly List<IObjectDesc> _childs = new List<IObjectDesc>();

        public CatalogDescriptor(string globalPath): base(globalPath)
        {
            IsMainCatalog = true;
        }

        public CatalogDescriptor(ICatalogDesc parent, string name): base(parent, name)
        {

        }

        public IReadOnlyCollection<IObjectDesc> Childs => _childs;

        public IReadOnlyCollection<IObjectDesc> AllChilds => GetAllObjects(this, new List<IObjectDesc>());

        public bool IsMainCatalog { get; }

        void ICatalogDesc.AddObject(IObjectDesc desc)
        {
            _childs.Add(desc);
        }

        void ICatalogDesc.RemoveObject(IObjectDesc desc)
        {
            _childs.Remove(desc);
        }

        private IReadOnlyCollection<IObjectDesc> GetAllObjects(ICatalogDesc catalog, List<IObjectDesc> list)
        {
            var subCatalogs = _childs.OfType<ICatalogDesc>();
            var filledCatalogs = subCatalogs.Where(x => x.Childs.Any());

            foreach (var cat in filledCatalogs)
            {
                list.AddRange(cat.AllChilds);
            }

            list.AddRange(_childs.OfType<IFileDesc>());
            list.AddRange(subCatalogs.Except(filledCatalogs));
            return list;
        }

    }
}
