using System;
using System.Collections.Generic;
using System.Text;
using SharpCompress.Archives;
using SharpCompress.Common;
using CbrOperations.Configuration;
using CbrOperations.Decoupling.SharpCompress;

namespace CbrOperations
{
    public interface ICbrExtract
    {
        IOneCbrFile OneFile { get; }
        ICbrExtractOutput Extract(ICbrInput input);
        ICbrExtractOutput Extract(IOneCbrFile oneFile);
        IArchiveFactoryDecoupling FactoryDecoupling { get; }
    }

    public class CbrExtract : ICbrExtract
    {
        private readonly IExtractionRules _extractionRules;
        public IArchiveFactoryDecoupling FactoryDecoupling { get; }
        public IOneCbrFile OneFile { get; }



        public CbrExtract(IExtractionRules extractionRules, IArchiveFactoryDecoupling archiveFactoryDecoupling)
        {
            _extractionRules = extractionRules;
            FactoryDecoupling = archiveFactoryDecoupling;
        }



        public ICbrExtractOutput Extract(IOneCbrFile oneFile)
        {
            ICbrExtractOutput output = new CbrExtractOutput();
            output.HasFolder = _extractionRules.ForceSubFolder;
            output.Files = FactoryDecoupling.WriteToFiles(System.IO.Path.GetDirectoryName(oneFile.FileName));

            return output;
        }



        public ICbrExtractOutput ExtractV1(IOneCbrFile oneFile)
        {
            string extractedFolder = System.IO.Path.GetDirectoryName(oneFile.FileName);

            ICbrExtractOutput output = new CbrExtractOutput();
            output.HasFolder = _extractionRules.ForceSubFolder;

            var archive = ArchiveFactory.Open(oneFile.FileName);

            foreach (var item in archive.Entries)
            {
                string fullFile = System.IO.Path.Combine(extractedFolder, item.Key);
                item.WriteToFile(fullFile);
                output.Files.Add(fullFile);
            }

            return output;
        }


        public ICbrExtractOutput Extract(ICbrInput input)
        {
            throw new NotImplementedException();
        }
    }
}
