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
        public void WriteToFile(string destinationFileName)
        {
            throw new NotImplementedException();
        }

        public string Key { get; set; }
    }
}
