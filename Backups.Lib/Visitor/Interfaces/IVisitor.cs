using Backups.Lib.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backups.Lib.Visitor
{
    public interface IVisitor
    {

        /// <summary>
        /// Сюда приходит пустая папка
        /// </summary>
        /// <param name="cat">пустая папка</param>
        void Visit(ICatalogDesc cat);
        void Visit(IFileDesc file);
    }
}
