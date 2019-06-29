using System;
using System.Collections.Generic;
using System.Text;
using DotNetDecoupling.System.IO;

namespace CbrOperations
{
    public interface IOneCbrFile
    {
        /// <summary>
        /// 
        /// </summary>
        string FileName { get; set; }
    }

    public class OneCbrFile : IOneCbrFile
    {
        private readonly IFile _file;

        public OneCbrFile(IFile file, string fileName)
        {
            _file = file;
            _fileName = fileName;
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                if (!_file.Exists(_fileName))
                    throw new System.IO.FileNotFoundException("cbr or cbz must exist");
                return _fileName;
            }
            set
            {
                if (!_file.Exists(_fileName))
                    throw new System.IO.FileNotFoundException("cbr or cbz must exist");
                _fileName = value;
            }
        }
    }
}
