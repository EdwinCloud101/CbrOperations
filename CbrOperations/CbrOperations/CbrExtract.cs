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

        public CbrExtract(IExtractionRules extractionRules, IArchiveFactoryDecoupling archiveFactoryDecoupling)
        {
            _extractionRules = extractionRules;
            FactoryDecoupling = archiveFactoryDecoupling;
        }

        public IOneCbrFile OneFile { get; }

        public ICbrExtractOutput Extract(IOneCbrFile oneFile)
        {
            string extractedFolder = System.IO.Path.GetDirectoryName(oneFile.FileName);

            ICbrExtractOutput output = new CbrExtractOutput();
            output.HasFolder = _extractionRules.ForceSubFolder;

            foreach (IRarEntry item in FactoryDecoupling.OpenAndGetEntries())
            {
                string fullFile = System.IO.Path.Combine(extractedFolder, item.Key);
                item.WriteToFile(fullFile);
                output.Files.Add(fullFile);
            }

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
            //bool hasFolder = input.HasFolder;
            //string extractedFolder = System.IO.Path.GetDirectoryName(input.Path);
            //string sourceFileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(input.Path);

            //if (hasFolder)
            //{
            //    extractedFolder = System.IO.Path.Combine(extractedFolder, sourceFileNameNoExtension + (input.UniqueFolderName? " - " + Guid.NewGuid() : ""));
            //    System.IO.Directory.CreateDirectory(extractedFolder);
            //}


            //ICbrExtractOutput output = new CbrExtractOutput();
            //output.HasFolder = hasFolder;


            //var archive = ArchiveFactory.Open(input.Path);


            //foreach (var item in archive.Entries)
            //{
            //    string fullFile = System.IO.Path.Combine(extractedFolder, item.Key);
            //    item.WriteToFile(fullFile);
            //    output.Files.Add(fullFile);
            //}

            //return output;

            return null;
        }


    }
}
