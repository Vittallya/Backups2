using Backups.Lib.Descriptors;
using Backups.Lib.Repository;

namespace Backups.Lib.BackupSystem
{
    public class BackupObject
    {

        public BackupObject(IObjectDesc x, IRepository repository)
        {
            this.Obj = x;
            Repository = repository;
        }

        public string RelativePathToFile { get; }
        public IRepository Repository { get; }
        public IObjectDesc Obj { get; }
        public IObjectDesc GetObject() => Repository.GetObject(RelativePathToFile);
    }
}