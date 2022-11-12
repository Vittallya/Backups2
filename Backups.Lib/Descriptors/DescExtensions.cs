using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backups.Lib.Descriptors
{
    static class DescExtensions
    {
        public static TCat GetOrCreateCatalog<TCat>(this TCat cat, string path, ICatalogDescFactory<TCat> descFactory)
            where TCat: class, ICatalogDesc
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path is null or empty");

            Queue<string> parts = new Queue<string>(path.Split('\\'));

            TCat cur = default;
            TCat parent = cat;

            // abc//bcd//

            while(parts.TryDequeue(out string part))
            {
                if (part.Length == 0)
                    continue;

                cur = parent.SubCatalogs.Where(x => x.Name == part) as TCat;

                if(cur == null)
                {
                    cur = descFactory.Create(parent, part);
                    parent.AddObject(cur);
                }
                parent = cur;
            }

            return cur;
        }

        /// <summary>
        /// Поиск подкаталога внутри каталога
        /// </summary>
        /// <typeparam name="TCat">Тип данных каталога</typeparam>
        /// <param name="parent">Сам родительский каталог</param>
        /// <param name="path">Путь относительно родительского каталога</param>
        /// <param name="current">Найденный подкаталог</param>
        /// <returns></returns>
        public static bool TryGetCatalog<TCat>(this TCat parent, string path, out TCat current)
            where TCat : class, ICatalogDesc
        {
            //todo через расширения - плохой способ, либо, надо учесть, что может быть не root папка
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path is null or empty");

            Queue<string> parts = new Queue<string>(path.Split('\\'));

            current = parent;

            // abc//bcd//

            while (parts.TryDequeue(out string part))
            {
                if (part.Length == 0)
                    continue;

                current = parent.SubCatalogs.Where(x => x.Name == part) as TCat;

                if (current == null)
                    return false;
            }

            return true;

        }

        /// <summary>
        /// Поиск файла внутри каталога
        /// </summary>
        /// <typeparam name="TChild">Тип данных каталога</typeparam>
        /// <param name="parent">Сам родительский каталог</param>
        /// <param name="path">Путь относительно родительского каталога</param>
        /// <param name="current">Найденный файл</param>
        /// <returns></returns>
        public static bool TryGetFile<TCat, TFile>(this TCat parent, string path, out TFile current, out TCat parentCat)
            where TFile : class, IFileDesc
            where TCat : class, ICatalogDesc
        {
            int lastIndexSlash = path.LastIndexOf('\\');
            if (string.IsNullOrEmpty(path) || lastIndexSlash == path.Length - 1)
                throw new ArgumentException("path is null or empty");

            current = default;
            string fileName = default;
            parentCat = parent;

            if (path.Contains('\\'))
            {
                //ищем конечную папку, содержащую файл
                fileName = path[(lastIndexSlash + 1)..];
                string pathToFile = path[..lastIndexSlash];
                if (!parent.TryGetCatalog(pathToFile, out parentCat))
                    return false;
            }

            current = parentCat.SubFiles.FirstOrDefault(x => x.Name == fileName) as TFile;

            return current != null;
        }


        public static TFile CreateFile<TCat, TFile>(this TCat cat, string path, Func<Stream> func, 
            ICatalogDescFactory<TCat> catalogFactory, IFileDescFactory<TCat, TFile> fileFactory )
            where TCat: class, ICatalogDesc
            where TFile : class, IFileDesc
        {

            int lastIndexSlash = path.LastIndexOf('\\');

            if (string.IsNullOrEmpty(path) || lastIndexSlash == path.Length - 1)
                throw new ArgumentException("Incorrect path");

            TFile file = null;

            if (path.Contains('\\'))
            {
                string fileName = path.Substring(lastIndexSlash + 1);
                string pathToFile = path.Substring(0, lastIndexSlash);

                if(cat.TryGetCatalog(path, out TCat cur))
                {
                    if (cur.SubFiles.Any(x => x.Name == fileName))
                        throw new ArgumentException("file with same name is exists");

                }
                else
                {
                    cur = cat.GetOrCreateCatalog(path, catalogFactory);
                }
                file = fileFactory.Create(cur, fileName, func);
                cur.AddObject(file);
            }
            else
            {

                file = fileFactory.Create(cat, path, func);
                cat.AddObject(file);
            }


            return file;
        }
    }
}
