using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Repository
{
    class FileSystemRepository : IRepository
    {
        public void CreateCatalog(string path)
        {
            Directory.CreateDirectory(path);
        }

        public IObjectDesc GetObject(string path)
        {
            throw new NotImplementedException();
        }

        public Stream GetReadStream(string path)
        {
            return File.OpenRead(path);
        }

        public Stream GetWriteStream(string path)
        {
            return File.OpenWrite(path);
            //return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public bool IsCatalogExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool IsFileExists(string path)
        {
            return File.Exists(path);
        }

        public byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void WriteFile(byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }
    }
}
