using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations
{
    public class CbrExtractOutput
    {
        public CbrExtractOutput()
        {
            this.Files = new List<string>();
        }

        public bool HasFolder { get; set; }
        public string OriginalCbrFileName { get; set; }
        public string FullSourcePath { get; set; }
        public string FullDestinationPath { get; set; }

        public List<string> Files { get; set; }
    }
}
