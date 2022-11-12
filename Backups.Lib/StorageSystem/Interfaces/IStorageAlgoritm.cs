using Backups.Lib.ArchiveSystem;
using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    public interface IStorageAlgorithm
    {
        /// <summary>
        /// Относительный путь сохранения архивов
        /// </summary>
        string BackupRelativePath { get; }

        /// <summary>
        /// Репозиторий конкретно для архивов
        /// </summary>
        IRepository StorageRepo { get; }

        /// <summary>
        /// То, как сохранять архивы бэкапов
        /// </summary>
        IZipArchiver Archiver { get; }

        /// <summary>
        /// Создание нового стораджа (архива или набора архивов) в текущем бэкап-репозитории
        /// </summary>
        /// <param name="objects">Объекты для сохрания в сторадж</param>
        /// <param name="backupName">Имя стораджа</param>
        /// <param name="restorePointName">Имя папки для конерктной т. восстановления</param>
        /// <param name="objectsRepo">Репозиторий для самих исходных файлов</param>
        /// <returns></returns>
        IStorage Create(IEnumerable<IObjectDesc> objects, string backupName, string restorePointName, IRepository objectsRepo);


        /// <summary>
        /// Изменения относительного пути хранения архивов в рамках текущего репозитория
        /// </summary>
        /// <param name="newRelativePath"></param>
        void ChangePath(string newRelativePath);

        /// <summary>
        /// Смена репозитория с указанием относительного пути для хранения архивов в рамках нового репо
        /// </summary>
        /// <param name="newRepo"></param>
        /// <param name="newRelativePath"></param>
        void ChangePath(IRepository newRepo, string newRelativePath);

        void ChangeArchiver(IZipArchiver newArchiver);
    }
}
