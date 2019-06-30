using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations.Decoupling.SharpCompress
{
    public interface IRarEntry
    {
        void WriteToFile(string destinationFileName);
        string Key { get; set; }
    }

    public class RarEntry : IRarEntry
    {
        private readonly IArchiveFactoryDecoupling _archiveFactoryDecoupling;

        public RarEntry(IArchiveFactoryDecoupling archiveFactoryDecoupling)
        {
            _archiveFactoryDecoupling = archiveFactoryDecoupling;
        }

        public void WriteToFile(string destinationFileName)
        {
            
        }

        public string Key { get; set; }
    }
}
