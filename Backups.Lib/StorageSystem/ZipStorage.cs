using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    class ZipStorage: IStorage
    {
        private IObjectDesc x;

        public ZipStorage(IRepository storageRepo, string relativePathToArchive, IEnumerable<IObjectDesc> objects)
        {
            StorageRepo = storageRepo;
            RelativePathToArchive = relativePathToArchive;
            ZipObjects = GetZipStructure(objects);
        }

        public ZipStorage(IRepository storageRepo, string relativePathToArchive, IObjectDesc x): 
            this(storageRepo, relativePathToArchive, new List<IObjectDesc> { x })
        {
        }

        private IEnumerable<IZipObject> GetZipStructure(IEnumerable<IObjectDesc> objects)
        {
            return objects.Select(x => GetZipObject(x));
        }

        private IZipObject GetZipObject(IObjectDesc obj)
        {
            if (obj is IFileDesc)
            {
                return new ZipFile(obj.Name);
            }
            else if(obj is ICatalogDesc cat)
            {
                return GetZipCatalog(cat);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private ZipCatalog GetZipCatalog(ICatalogDesc target)
        {
            var childsList = new List<IZipObject>();

            target.SubCatalogs.ToList().ForEach(subCatalog =>
            {
                childsList.Add(GetZipCatalog(subCatalog));
            });

            var subFiles = target.SubFiles.Select(x => GetZipObject(x));
            childsList.AddRange(subFiles);
            return new ZipCatalog(target.Name, childsList);
        }

        public IEnumerable<IObjectDesc> GetObjects(IRepository objectsRepo)
        {
            //todo зип сторадж: получение объектов обратно
            throw new NotImplementedException();
        }

        public IRepository StorageRepo { get; }
        public string RelativePathToArchive { get; }
        public string[] AllFilePath { get; }
        public IEnumerable<IZipObject> ZipObjects { get; }
    }
}
