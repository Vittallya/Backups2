using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Repository
{
    class InMemoryRepository : IRepository
    {
        public void CreateCatalog(string path)
        {
            throw new NotImplementedException();
        }

        public IObjectDesc GetDescriptor(string path)
        {
            throw new NotImplementedException();
        }

        public Stream GetWriteStream(string path)
        {
            throw new NotImplementedException();
        }

        public bool IsCatalogExists(string path)
        {
            throw new NotImplementedException();
        }

        public bool IsFileExists(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadFile(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(byte[] data, string path)
        {
            throw new NotImplementedException();
        }
    }
}
