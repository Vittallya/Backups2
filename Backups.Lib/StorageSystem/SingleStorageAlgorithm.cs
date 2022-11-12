using Backups.Lib.ArchiveSystem;
using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    class SingleStorageAlgorithm : StorageAlgorithm
    {
        public SingleStorageAlgorithm(string backupRelativePath, IRepository storageRepo, IZipArchiver archiver) 
            : base(backupRelativePath, storageRepo, archiver)
        {
        }

        public override IStorage Create(IEnumerable<IObjectDesc> objects, string backupTaskName, string restorePointName, IRepository objectsRepo)
        {
            string relativePathToArchive = Path.Combine(BackupRelativePath, backupTaskName, restorePointName, restorePointName);
            Archiver.CreateArchive(objects, relativePathToArchive, StorageRepo);
            return new ZipStorage(StorageRepo, relativePathToArchive, objects);
        }
    }
}
