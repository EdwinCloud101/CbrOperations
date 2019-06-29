using System;
using System.Collections.Generic;
using System.Text;
using SharpCompress.Archives;

namespace CbrOperations.Decoupling.SharpCompress
{
    public interface IArchiveFactoryDecoupling
    {
        string FileName { get; }
        IList<IRarEntry> OpenAndGetEntries();
    }

    public class ArchiveFactoryDecoupling : IArchiveFactoryDecoupling
    {
        private readonly List<IRarEntry> _rarEntries;

        public ArchiveFactoryDecoupling(string fileName, List<IRarEntry> rarEntries)
        {
            _rarEntries = rarEntries;
            FileName = fileName;
        }


        public string FileName { get; }

        public IList<IRarEntry> OpenAndGetEntries()
        {
            IList<IRarEntry> list = new List<IRarEntry>();

            var archive = ArchiveFactory.Open(FileName);
            foreach (var archiveEntry in archive.Entries)
            {
                IRarEntry item = new RarEntry();
                item.Key = item.Key;
                list.Add(item);
            }

            return list;
        }

        //public IEnumerable<IArchiveEntry> OpenAndGetEntries()
        //{
        //    var archive = ArchiveFactory.Open(FileName);
        //    return archive.Entries;
        //}
    }

}
