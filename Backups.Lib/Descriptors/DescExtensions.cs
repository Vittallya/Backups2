using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backups.Lib.Descriptors
{
    static class DescExtensions
    {
        public static TCat GetOrCreateCatalog<TCat>(this TCat cat, string path, Func<TCat, string, TCat> ctor)
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
                    cur = ctor(parent, part);
                    parent.AddObject(cur);
                }
                parent = cur;
            }

            return cur;
        }

        public static bool TryGetCatalog<TCat>(this TCat cat, string path, out TCat current)
            where TCat : class, ICatalogDesc
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("path is null or empty");

            Queue<string> parts = new Queue<string>(path.Split('\\'));

            current = cat;

            // abc//bcd//

            while (parts.TryDequeue(out string part))
            {
                if (part.Length == 0)
                    continue;

                current = cat.SubCatalogs.Where(x => x.Name == part) as TCat;

                if (current == null)
                    return false;
            }

            return true;

        }

        public static TFile CreateFile<TCat, TFile>(this TCat cat, string path, Func<Stream> func, 
            Func<TCat, string, TCat> catalogCtor, Func<TCat, string, Func<Stream>, TFile> fileCtor )
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
                    cur = cat.GetOrCreateCatalog(path, catalogCtor);
                }
                file = fileCtor(cur, fileName, func);
                cur.AddObject(file);
            }
            else
            {

                file = fileCtor(cat, path, func);
                cat.AddObject(file);
            }


            return file;
        }
    }
}
