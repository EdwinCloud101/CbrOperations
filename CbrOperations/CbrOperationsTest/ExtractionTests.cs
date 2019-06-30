using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CbrOperations;
using CbrOperations.Configuration;
using CbrOperations.Decoupling.SharpCompress;
using DotNetDecoupling.System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CbrOperationsUnitTests
{
    [TestClass]
    public class ExtractionTests
    {
        [TestMethod]
        public void ExtractUnitTest()
        {
            var container = new WindsorContainer();

            string fileName = @"D:\Temp\Weird War Tales 025.cbr";

            var fileMock = new Mock<IFile>();
            fileMock.Setup(library => library.Exists(It.IsAny<string>())).Returns(true);
            container.Register(Component.For<IFile>().Instance(fileMock.Object));

            container.Register(Component.For<IOneCbrFile>().ImplementedBy<OneCbrFile>().DependsOn(Dependency.OnValue<string>(fileName)));




            var archiveMock = new Mock<IArchiveFactoryDecoupling>();
            List<string> list = new List<string>();
            list.Add("some_file.jpg");

            archiveMock.Setup(lib => lib.WriteToFiles(It.IsAny<string>())).Returns(list);
            archiveMock.Setup(lib => lib.FileName).Returns(fileName);

            container.Register(Component.For<IArchiveFactoryDecoupling>().Instance(archiveMock.Object));





            container.Register(Component.For<IExtractionRules>().ImplementedBy<ExtractionRules>());
            container.Register(Component.For<ICbrExtract>().ImplementedBy<CbrExtract>());





            var extractionRules = container.Resolve<IExtractionRules>();
            var extract = container.Resolve<ICbrExtract>();
            var oneFile = container.Resolve<IOneCbrFile>();

            var output = extract.Extract(oneFile);

            Assert.IsTrue(extractionRules != null);
            Assert.IsTrue(extractionRules is ExtractionRules);
            Assert.IsTrue(extract != null);
            Assert.IsTrue(extract is CbrExtract);
            Assert.IsTrue(output != null);
            Assert.IsTrue(output is CbrExtractOutput);
            Assert.IsTrue(output.Files.Count == 1);
            Assert.IsTrue(output.Files[0].Contains("some_file.jpg"));
        }
    }
}
