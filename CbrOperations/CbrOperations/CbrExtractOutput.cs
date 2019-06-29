using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations
{

    public interface ICbrExtractOutput
    {
        string ExtractedPath { get; set; }
        bool HasFolder { get; set; }

        string FolderName { get; set; }

        List<string> Files { get; set; }
    }

    public class CbrExtractOutput : ICbrExtractOutput
    {
        public CbrExtractOutput()
        {
            this.Files = new List<string>();
        }

        public string ExtractedPath { get; set; }
        public bool HasFolder { get; set; }
        public string FolderName { get; set; }

        public List<string> Files { get; set; }
    }
}
