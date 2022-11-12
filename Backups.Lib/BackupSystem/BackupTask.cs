using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using Backups.Lib.StorageSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.BackupSystem
{
    class BackupTask : IBackupTask
    {
        private readonly List<IObjectDesc> currentTrackingObjects = new List<IObjectDesc>();

        public BackupTask(string name, IRepository repository, IStorageAlgorithm algorithm)
        {
            Name = name;
            Repository = repository;
            Algorithm = algorithm;
        }

        public string Name { get; }
        public IRepository Repository { get; }
        public IStorageAlgorithm Algorithm { get; }

        public void ClearTrackingForObject(string relativePathToObj)
        {
            throw new NotImplementedException();
        }

        public void ClearTrackingForObject(IObjectDesc obj)
        {
            //todo слежение за объектами:вложенность
            throw new NotImplementedException();
        }

        public void CreateBackup()
        {
            throw new NotImplementedException();
        }

        public void StartTrackingForObject(string relativePathToObj)
        {
            var obj = Repository.GetObject(relativePathToObj);
            StartTrackingForObject(obj);
        }

        public void StartTrackingForObject(IObjectDesc obj)
        {
            if (!currentTrackingObjects.Contains(obj))
                currentTrackingObjects.Add(obj);
        }
    }
}
