using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetDecoupling.System.IO;

namespace CbrOperations.Input
{
    public interface IPathOperations
    {
        List<string> GetSourceCbrFiles(CbrPaths paths);
        List<string> GetDestinationCbrFiles(CbrPaths paths);
    }


    public class PathOperations : IPathOperations
    {
        private readonly IDirectory _directory;

        public PathOperations(IDirectory directory)
        {
            _directory = directory;
        }

        public List<string> GetSourceCbrFiles(CbrPaths paths)
        {
            var files = _directory.GetFiles(paths.SourcePath, "*.*", paths.SourceSearchOption).Where(f => f.EndsWith(".cbr") || f.EndsWith(".cbz"));
            return files.ToList();
        }

        public List<string> GetDestinationCbrFiles(CbrPaths paths)
        {
            var files = _directory.GetFiles(paths.DestinationPath, "*.*", paths.SourceSearchOption).Where(f => f.EndsWith(".cbr") || f.EndsWith(".cbz"));
            return files.ToList();
        }
    }
}
