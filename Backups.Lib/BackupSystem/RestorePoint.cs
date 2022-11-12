using Backups.Lib.StorageSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.BackupSystem
{
    class RestorePoint
    {
        public RestorePoint(DateTime creationDateTime, 
            IReadOnlyCollection<BackupObject> backupObjects, 
            IStorage storage, 
            int number)
        {
            CreationDateTime = creationDateTime;
            BackupObjects = backupObjects;
            Storage = storage;
            Number = number;
        }

        public DateTime CreationDateTime { get; }
        public IReadOnlyCollection<BackupObject> BackupObjects { get; }
        public IStorage Storage { get; }
        public int Number { get; }
    }
}
