using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Descriptors
{
    public interface IFileDesc: IObjectDesc
    {
        public string NameWithoutExt { get; }
        public string Ext { get; }

        public Stream GetStream();

        internal void SetStream(Func<Stream> func);
    }
}
