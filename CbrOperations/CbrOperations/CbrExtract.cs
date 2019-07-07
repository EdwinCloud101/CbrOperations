using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCompress.Archives;
using SharpCompress.Common;
using CbrOperations.Configuration;
using CbrOperations.Decoupling.SharpCompress;
using DotNetDecoupling.System.IO;

namespace CbrOperations
{
    public interface ICbrExtract
    {
        List<CbrExtractOutput> Extract(CbrPaths paths);
        IArchiveFactoryDecoupling FactoryDecoupling { get; }
    }

    public class CbrExtract : ICbrExtract
    {
        private readonly ExtractionRules _extractionRules;
        private readonly IDirectory _directory;
        private readonly IPath _path;


        public IArchiveFactoryDecoupling FactoryDecoupling { get; }


        public CbrExtract(ExtractionRules extractionRules, IArchiveFactoryDecoupling archiveFactoryDecoupling, IDirectory directory, IPath path)
        {
            _extractionRules = extractionRules;
            _directory = directory;
            _path = path;
            FactoryDecoupling = archiveFactoryDecoupling;
        }



        public List<CbrExtractOutput> Extract(CbrPaths paths)
        {
            var outputList = new List<CbrExtractOutput>();
            
            var files = _directory.GetFiles(paths.SourcePath, "*.*", paths.SourceSearchOption).Where(f => f.EndsWith(".cbr") || f.EndsWith(".cbz"));
            foreach (var item in files)
            {
                string finalDestination = paths.DestinationPath;

                if (_extractionRules.ForceSubFolder)
                {
                    finalDestination = _path.Combine(finalDestination, _path.GetFileName(item).Replace(_path.GetExtension(item),""));
                    _directory.CreateDirectory(finalDestination);
                }


                var output = new CbrExtractOutput();
                output.HasFolder = _extractionRules.ForceSubFolder;
                output.OriginalCbrFileName = _path.GetFileName(item);
                //TODO: consider subfolder approach
                output.FullSourcePath = paths.SourcePath;
                output.FullDestinationPath = paths.DestinationPath;
                output.Files = FactoryDecoupling.WriteToFiles(finalDestination, item);
                outputList.Add(output);
            }

            return outputList;
        }



    }
}
