using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCompress.Archives;

namespace CbrOperations.Decoupling.SharpCompress
{
    public interface IArchiveFactoryDecoupling
    {
        string FileName { get; }
        IArchive CurrentArchive { get; }
        List<string> WriteToFiles(string extractedFolder);
    }

    public class ArchiveFactoryDecoupling : IArchiveFactoryDecoupling
    {
        public string FileName { get; }
        public IArchive CurrentArchive { get; private set; }
        public IList<IRarEntry> Entries { get; private set; }



        public ArchiveFactoryDecoupling(string fileName)
        {
            FileName = fileName;
        }



        public List<string> WriteToFiles(string extractedFolder)
        {
            var list = new List<string>();

            CurrentArchive = ArchiveFactory.Open(FileName);
            foreach (var item in CurrentArchive.Entries)
            {
                string fullFile = System.IO.Path.Combine(extractedFolder, item.Key);
                item.WriteToFile(fullFile);
                list.Add(item.Key);
            }

            return list;
        }



    }

}
