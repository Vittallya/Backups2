using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.BackupSystem
{
    public interface IBackupTask
    {
        /// <summary>
        /// Начать следить за объектом
        /// </summary>
        /// <param name="relativePathToObj">Относительный путь к объекту</param>
        void StartTrackingForObject(string relativePathToObj);
        void StartTrackingForObject(IObjectDesc obj);
        void ClearTrackingForObject(string relativePathToObj);
        void ClearTrackingForObject(IObjectDesc obj);
        /// <summary>
        /// Выполнить бэкап
        /// </summary>
        void CreateBackup();
    }
}
