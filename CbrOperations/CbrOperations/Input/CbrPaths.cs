using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CbrOperations
{
    //public interface ICbrPaths
    //{
    //    System.IO.SearchOption SourceSearchOption { get; set; }
    //    string SourcePath { get; set; }
    //    string DestinationPath { get; set; }
    //    bool EnforceDestinationSubfolder { get; set; }
        
    //    /// <summary>
    //    /// Folder name
    //    /// </summary>
    //    string EnforcedDestinationName { get; set; }
    //}

    public class CbrPaths //: ICbrPaths
    {
        public SearchOption SourceSearchOption { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool EnforceDestinationSubfolder { get; set; }
        public string EnforcedDestinationName { get; set; }

        public CbrPaths()
        {
            //
        }
    }
}
