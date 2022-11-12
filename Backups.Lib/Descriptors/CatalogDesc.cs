using Backups.Lib.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.Descriptors
{
    class CatalogDesc : ObjectDescr, ICatalogDesc
    {
        private readonly List<IObjectDesc> _childs = new List<IObjectDesc>();

        public CatalogDesc(string globalPath): base(globalPath)
        {
            IsMainCatalog = true;
        }

        public CatalogDesc(ICatalogDesc parent, string name): base(parent, name)
        {

        }

        public IReadOnlyCollection<IObjectDesc> Childs => _childs;

        public IReadOnlyCollection<IObjectDesc> AllChilds => GetAllObjects(this, new List<IObjectDesc>());

        public bool IsMainCatalog { get; }

        public IReadOnlyCollection<ICatalogDesc> SubCatalogs => _childs.OfType<ICatalogDesc>().ToList();

        public IReadOnlyCollection<IFileDesc> SubFiles => _childs.OfType<IFileDesc>().ToList();

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
            var subCatalogs = SubCatalogs;
            var filledCatalogs = subCatalogs.Where(x => x.Childs.Any());

            foreach (var cat in filledCatalogs)
            {
                list.AddRange(cat.AllChilds);
            }

            list.AddRange(SubFiles);
            list.AddRange(subCatalogs.Except(filledCatalogs));
            return list;
        }

        public override void Accept(IVisitor visitor)
        {
            if (_childs.Any())
                _childs.ForEach(x => x.Accept(visitor));
            else
                visitor.Visit(this);
        }
    }
}
