using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations
{
    public interface ICbrInput
    {

        string SourcePath { get; }

        string DestinationPath { get; }
    }

    public class CbrInput : ICbrInput
    {
        public CbrInput(ICbrPaths cbrPaths)
        {
            SourcePath = cbrPaths.SourcePath;
            DestinationPath = cbrPaths.DestinationPath;
        }

        public CbrInput(IOneCbrFile oneCbrFile)
        {

        }
        
        public string SourcePath { get; private set; }

        public string DestinationPath { get; private set; }

    }
}
