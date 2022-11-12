using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backups.Lib.Repository
{
    public class FileSystemRepository : IRepository
    {
        private readonly string absolutePathToRootCatalog;

        public string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(absolutePathToRootCatalog, relativePath);
        }

        public void CheckCreateCatalog(string relativePath)
        {
            if (!IsCatalogExists(Path.GetDirectoryName(relativePath)))
                CreateCatalog(Path.GetDirectoryName(relativePath));
        }

        //todo репо:абсолютные и относительные пути
        public FileSystemRepository(string rootCatalog)
        {
            this.absolutePathToRootCatalog = rootCatalog;
        }

        public void CreateCatalog(string relativePath)
        {
            Directory.CreateDirectory(GetAbsolutePath(relativePath));
        }

        public IObjectDesc GetObject(string relativePath)
        {

            ICatalogDesc root = new CatalogDesc(absolutePathToRootCatalog);

            if (IsCatalogExists(relativePath))
            {
                bool isRoot = relativePath.Length == 0 || relativePath.Length == 1 && relativePath[0] == '\\';

                //todo проверить, если будет отправлен в параметре только '/'
                var path = GetAbsolutePath(relativePath);

                DirectoryInfo di = new DirectoryInfo(path);
                var allObjects = di.EnumerateFileSystemInfos("*",
                    new EnumerationOptions { RecurseSubdirectories = true }).ToList();


                allObjects.ForEach(x =>
                {
                    if (x.Attributes == FileAttributes.Directory)
                    {
                        root.GetOrCreateCatalog(x.FullName[root.PathGlobal.Length..],
                            new CatalogDescFactory());
                    }
                    else
                    {
                        root.CreateFile(x.FullName[root.PathGlobal.Length..],
                            () => File.Open(x.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                            new CatalogDescFactory(),
                            new FileDescFactory());
                    }
                });

                return isRoot ? root : root.GetOrCreateCatalog(relativePath, new CatalogDescFactory());
            }

            else if (IsFileExists(relativePath))
            {
                FileInfo info = new FileInfo(GetAbsolutePath(relativePath));

                return root.CreateFile(relativePath, () => File.Open(info.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                    new CatalogDescFactory(), new FileDescFactory());
            }
            else
            {
                throw new ArgumentException("incorrect path");
            }
        }

        public Stream GetReadStream(string path)
        {
            return File.OpenRead(GetAbsolutePath(path));
        }

        public Stream GetWriteStream(string path)
        {
            CheckCreateCatalog(path);
            return File.OpenWrite(GetAbsolutePath(path));
            //return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public bool IsCatalogExists(string path)
        {
            return Directory.Exists(GetAbsolutePath(path));
        }

        public bool IsFileExists(string path)
        {
            return File.Exists(GetAbsolutePath(path));
        }

        public byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(GetAbsolutePath(path));
        }

        public void WriteFile(byte[] data, string path)
        {
            CheckCreateCatalog(path);
            File.WriteAllBytes(GetAbsolutePath(path), data);
        }
    }
}
