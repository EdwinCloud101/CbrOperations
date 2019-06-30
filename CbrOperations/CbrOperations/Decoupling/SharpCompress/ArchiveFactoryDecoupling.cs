using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCompress.Archives;

namespace CbrOperations.Decoupling.SharpCompress
{
    public interface IArchiveFactoryDecoupling
    {
        IArchive CurrentArchive { get; }
        List<string> WriteToFiles(string extractedFolder, string fileName);
    }

    public class ArchiveFactoryDecoupling : IArchiveFactoryDecoupling
    {
        
        public IArchive CurrentArchive { get; private set; }
        public IList<IRarEntry> Entries { get; private set; }



        public ArchiveFactoryDecoupling()
        {
            
        }


        public List<string> WriteToFiles(string extractedFolder, string fileName)
        {
            var list = new List<string>();

            CurrentArchive = ArchiveFactory.Open(fileName);
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