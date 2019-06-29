using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations
{
    public interface ICbrPaths
    {
        string SourcePath { get; set; }
        string DestinationPath { get; set; }
    }

    public class CbrPaths : ICbrPaths
    {
        public CbrPaths()
        {
            //
        }

        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
}
