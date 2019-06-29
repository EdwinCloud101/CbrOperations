using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrOperations;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using CbrOperations.Configuration;
using CbrOperations.Decoupling.SharpCompress;
using DotNetDecoupling.System.IO;

namespace CbrOperationsIntegrationTests
{
    [TestClass]
    public class ExtractionIntegrationTests
    {
        [TestMethod]
        public void ExtractTest()
        {
            var container = new WindsorContainer();

            string fileName = @"D:\Temp\Weird War Tales 025.cbr";


            container.Register(Component.For<IFile>().ImplementedBy<File>().DependsOn(Dependency.OnValue<string>(fileName)));
            container.Register(Component.For<IOneCbrFile>().ImplementedBy<OneCbrFile>().DependsOn(Dependency.OnValue<string>(fileName)));


            container.Register(Component.For<List<IRarEntry>>().ImplementedBy<List<IRarEntry>>());
            container.Register(Component.For<IArchiveFactoryDecoupling>().ImplementedBy<ArchiveFactoryDecoupling>().DependsOn(Dependency.OnValue<string>(fileName)));
            container.Register(Component.For<IExtractionRules>().ImplementedBy<ExtractionRules>());
            container.Register(Component.For<ICbrExtract>().ImplementedBy<CbrExtract>());


            var extractionRules = container.Resolve<IExtractionRules>();
            var extract = container.Resolve<ICbrExtract>();
            var oneFile = container.Resolve<IOneCbrFile>();

            extract.Extract(oneFile);

            Assert.IsTrue(extractionRules != null);
            Assert.IsTrue(extractionRules is ExtractionRules);
            Assert.IsTrue(extract != null);
            Assert.IsTrue(extract is CbrExtract);
        }
    }
}
