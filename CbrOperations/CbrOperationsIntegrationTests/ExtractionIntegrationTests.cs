using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrOperations;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using CbrOperations.Configuration;
using CbrOperations.Decoupling.SharpCompress;
using DotNetDecoupling.System.IO;
using Moq;
using Directory = DotNetDecoupling.System.IO.Directory;
using File = DotNetDecoupling.System.IO.File;

namespace CbrOperationsIntegrationTests
{
    [TestClass]
    public class ExtractionIntegrationTests
    {

        [TestMethod]
        public void ExtractPathsUnitTest()
        {
            var container = new WindsorContainer();

            string fileName = @"D:\Temp";

            container.Register(Component.For<IArchiveFactoryDecoupling>().ImplementedBy<ArchiveFactoryDecoupling>().DependsOn(Dependency.OnValue<string>(fileName)));
            container.Register(Component.For<IDirectory>().ImplementedBy<Directory>());
            container.Register(Component.For<ExtractionRules>().ImplementedBy<ExtractionRules>());
            container.Register(Component.For<CbrPaths>().ImplementedBy<CbrPaths>());
            container.Register(Component.For<ICbrExtract>().ImplementedBy<CbrExtract>().DependsOn(Dependency.OnValue<string>(fileName)));

            var extractionRules = container.Resolve<ExtractionRules>();
            var extract = container.Resolve<ICbrExtract>();
            var paths = container.Resolve<CbrPaths>();
            paths.SourcePath = @"D:\Temp\pending";
            paths.DestinationPath = @"D:\Temp\working";

            var output = extract.Extract(paths);



            Assert.IsTrue(extractionRules != null);
            Assert.IsTrue(extractionRules is ExtractionRules);
            Assert.IsTrue(extract != null);
            Assert.IsTrue(extract is CbrExtract);
            Assert.IsTrue(output != null);
            Assert.IsTrue(paths.SourcePath != null);
            Assert.IsTrue(paths.DestinationPath != null);
            Assert.IsTrue(output is List<CbrExtractOutput>);
            Assert.IsTrue(output.Count == 1);
        }
    }
}