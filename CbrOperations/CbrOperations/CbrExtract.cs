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
        private readonly IExtractionRules _extractionRules;
        private readonly IDirectory _directory;



        public IArchiveFactoryDecoupling FactoryDecoupling { get; }



        public CbrExtract(IExtractionRules extractionRules, IArchiveFactoryDecoupling archiveFactoryDecoupling, IDirectory directory)
        {
            _extractionRules = extractionRules;
            _directory = directory;
            FactoryDecoupling = archiveFactoryDecoupling;
        }



        public List<CbrExtractOutput> Extract(CbrPaths paths)
        {
            var outputList = new List<CbrExtractOutput>();
            
            var files = _directory.GetFiles(paths.SourcePath, "*.*", paths.SourceSearchOption).Where(f => f.EndsWith(".cbr") || f.EndsWith(".cbz"));
            foreach (var item in files)
            {
                var output = new CbrExtractOutput();
                output.HasFolder = _extractionRules.ForceSubFolder;
                output.Files = FactoryDecoupling.WriteToFiles(paths.DestinationPath, item);
                outputList.Add(output);
            }

            return outputList;
        }



    }
}
