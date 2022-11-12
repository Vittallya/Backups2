using Backups.Lib.ArchiveSystem;
using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    public abstract class StorageAlgorithm : IStorageAlgorithm
    {
        protected StorageAlgorithm(string backupRelativePath, IRepository storageRepo, IZipArchiver archiver)
        {
            BackupRelativePath = backupRelativePath;
            StorageRepo = storageRepo;
            Archiver = archiver;
        }

        public string BackupRelativePath { get; private set; }

        public IRepository StorageRepo { get; private set; }

        public IZipArchiver Archiver { get; private set; }

        public void ChangeArchiver(IZipArchiver newArchiver)
        {
            Archiver = newArchiver;
        }

        public void ChangePath(string newRelativePath)
        {
            BackupRelativePath = newRelativePath;
        }

        public void ChangePath(IRepository newRepo, string newRelativePath)
        {
            StorageRepo = newRepo;
            BackupRelativePath = newRelativePath;
        }

        public abstract IStorage Create(IEnumerable<IObjectDesc> objects, string backupName, string restorePointName, IRepository objectsRepo);
    }
}
