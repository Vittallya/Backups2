using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using Backups.Lib.StorageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backups.Lib.BackupSystem
{
    public class BackupTask : IBackupTask
    {
        private readonly List<IObjectDesc> currentTrackingObjects = new List<IObjectDesc>();
        private readonly List<RestorePoint> restorePoints = new List<RestorePoint>();

        public IReadOnlyList<RestorePoint> RestorePoints => restorePoints;

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
            currentTrackingObjects.Where(x => x.RelativePath == relativePathToObj).ToList()
                .ForEach(x => ClearTrackingForObject(x));
        }

        public void ClearTrackingForObject(IObjectDesc obj)
        {
            //todo слежение за объектами:вложенность
            currentTrackingObjects.Remove(obj);
        }

        public void CreateBackup()
        {
            var storage = Algorithm.Create(currentTrackingObjects, Name,
                (restorePoints.Count + 1).ToString(), Repository);

            var backupObjects = currentTrackingObjects.Select(x => new BackupObject(x, Repository));
            var rp = new RestorePoint(DateTime.Now, backupObjects, storage, restorePoints.Count + 1);
            restorePoints.Add(rp);
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
