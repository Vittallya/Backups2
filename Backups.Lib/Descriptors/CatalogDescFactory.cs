using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.Descriptors
{
    class CatalogDescFactory : ICatalogDescFactory<ICatalogDesc>
    {
        public ICatalogDesc Create(string absolutePath)
        {
            return new CatalogDesc(absolutePath);
        }

        public ICatalogDesc Create(ICatalogDesc parent, string name)
        {
            return new CatalogDesc(parent, name);
        }
    }
}
