using System;
using System.Collections.Generic;
using System.Text;

namespace CbrOperations.Configuration
{
    public interface IExtractionRules
    {
        bool ForceSubFolder { get; }
        bool RenameEachPagetoIndex { get; }
    }

    public class ExtractionRules : IExtractionRules
    {
        public bool ForceSubFolder { get; }
        public bool RenameEachPagetoIndex { get; }
    }
}
