using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Repository
{

    //root/a/a/v/

    class InMemoryRepository : IRepository
    {
        private readonly ICatalogDesc root;

        public InMemoryRepository(string name)
        {
            root = new CatalogDesc(name);
        }


        /// <summary>
        /// создание каталога внутри корневой папки
        /// </summary>
        /// <param name="relativePath">относительный путь до каталога, не включая названия корневой папки</param>
        public void CreateCatalog(string relativePath)
        {
            root.GetOrCreateCatalog(relativePath, new CatalogDescFactory());
        }

        public IObjectDesc GetObject(string path)
        {
            if (root.TryGetFile(path, out IFileDesc file, out _))
            {
                return file;
            }
            else if(root.TryGetCatalog(path, out ICatalogDesc cat))
            {
                return cat;
            }
            else
            {
                throw new ArgumentException("incorrect path");
            }
        }

        public Stream GetReadStream(string path)
        {
            if(root.TryGetFile(path, out IFileDesc file, out _))
            {
                return file.GetStream();
            }
            throw new ArgumentException("incorrect path");
        }

        public Stream GetWriteStream(string path)
        {
            byte[] arr = new byte[1024000];
            MemoryStream str = new MemoryStream(arr, true);
            WriteFile(arr, path);
            return str;
        }

        public bool IsCatalogExists(string path)
        {
            return root.TryGetCatalog(path, out _);
        }

        public bool IsFileExists(string path)
        {
            return root.TryGetFile<ICatalogDesc, IFileDesc>(path, out _, out _);
        }

        public byte[] ReadFile(string path)
        {
            if(root.TryGetFile(path, out IFileDesc file, out _))
            {
                using var str = file.GetStream();
                using MemoryStream mem = new MemoryStream();
                str.CopyTo(mem);
                return mem.ToArray();
            }

            throw new ArgumentException("file was not found");
        }

        public void WriteFile(byte[] data, string path)
        {
            if (root.TryGetFile(path, out IFileDesc file, out _))
            {
                file.SetStream(() => new MemoryStream(data));
            }
            else
            {
                root.CreateFile(path, () => new MemoryStream(data), new CatalogDescFactory(), new FileDescFactory());
            }

        }
    }
}
