using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Descriptors
{
    class FileDescFactory : IFileDescFactory<ICatalogDesc, IFileDesc>
    {
        public IFileDesc Create(ICatalogDesc parent, string name, Func<Stream> func)
        {
            return new FileDesc(parent, name, func);
        }
    }
}
