using Backups.Lib.Descriptors;
using Backups.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.ArchiveSystem
{
    public interface IZipArchiver
    {
        void CreateArchive(IObjectDesc obj, string relativePathToArchive, IRepository targetRepo);
        void CreateArchive(IEnumerable<IObjectDesc> obj, string relativePathToArchive, IRepository targetRepo);
    }
}
