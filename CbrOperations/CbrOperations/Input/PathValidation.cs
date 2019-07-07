using System;
using System.Collections.Generic;
using System.Text;
using DotNetDecoupling.System.IO;

namespace CbrOperations.Input
{
    public interface IPathValidation
    {
        bool SourceExists(CbrPaths paths);
        bool DestinationExists(CbrPaths paths);
    }


    public class PathValidation : IPathValidation
    {
        private readonly IDirectory _directory;

        public PathValidation(IDirectory directory)
        {
            _directory = directory;
        }

        public bool SourceExists(CbrPaths paths)
        {
            return _directory.Exists(paths.SourcePath);
        }

        public bool DestinationExists(CbrPaths paths)
        {
            return _directory.Exists(paths.DestinationPath);
        }
    }
}
