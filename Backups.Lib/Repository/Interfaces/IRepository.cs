using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Repository
{
    public interface IRepository
    {
        bool IsCatalogExists(string path);
        bool IsFileExists(string path);
        void WriteFile(byte[] data, string path);
        byte[] ReadFile(string path);
        Stream GetWriteStream(string path);
        Stream GetReadStream(string path);
        void CreateCatalog(string path);
        IObjectDesc GetDescriptor(string path);
    }
}
