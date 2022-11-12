using Backups.Lib.Visitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backups.Lib.Descriptors
{
    class FileDesc : ObjectDescr, IFileDesc
    {
        private Func<Stream> func;

        public FileDesc(ICatalogDesc parent, string name, Func<Stream> func) : base(parent, name)
        {
            this.func = func;

            if (name.Contains('.'))
            {
                NameWithoutExt = name.Substring(0, name.LastIndexOf('.'));
                Ext = name.Substring(name.LastIndexOf('.'));
            }
            else
            {
                NameWithoutExt = name;
            }

        }

        public string NameWithoutExt { get; }

        public string Ext { get; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Stream GetStream()
        {
            return func();
        }

        void IFileDesc.SetStream(Func<Stream> func)
        {
            this.func = func;
        }
    }
}
