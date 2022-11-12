using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    class SplitStorage : IStorage
    {
        private readonly List<ZipStorage> zipStorages;

        public SplitStorage(IEnumerable<ZipStorage> zipStorages)
        {
            this.zipStorages = zipStorages.ToList();
        }

        public IEnumerable<IObjectDesc> GetObjects(IRepository objectsRepo)
        {
            return zipStorages.SelectMany(x => x.GetObjects(objectsRepo));
        }

        public int GetStoragesCount() => zipStorages.Count;
    }
}
