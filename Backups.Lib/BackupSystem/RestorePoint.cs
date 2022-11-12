using Backups.Lib.StorageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.BackupSystem
{
    public class RestorePoint
    {
        public RestorePoint(DateTime creationDateTime, 
            IEnumerable<BackupObject> backupObjects, 
            IStorage storage, 
            int number)
        {
            CreationDateTime = creationDateTime;
            BackupObjects = backupObjects.ToList();
            Storage = storage;
            Number = number;
        }

        public DateTime CreationDateTime { get; }
        public IReadOnlyCollection<BackupObject> BackupObjects { get; }
        public IStorage Storage { get; }
        public int Number { get; }
    }
}
