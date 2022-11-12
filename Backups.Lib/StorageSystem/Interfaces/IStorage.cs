using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.StorageSystem
{
    public interface IStorage
    {
        /// <summary>
        /// Получить содержимое стораджа (архива или набора архивов)
        /// </summary>
        /// <param name="objectsRepo">Репозиторий где хрянтся (-лись) исходные объекты</param>
        /// <returns></returns>
        IEnumerable<IObjectDesc> GetObjects(IRepository objectsRepo);
    }
}
