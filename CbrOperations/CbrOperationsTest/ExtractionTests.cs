using System;
using System.Collections.Generic;
using System.IO;
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
        public void ExtractOneFileUnitTest()
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




            var directoryMock = new Mock<IDirectory>();
            var listFiles = new List<string>();
            for (var i = 0; i < 1; i++)
            {
                listFiles.Add($"{Guid.NewGuid()}.cbr");
            }

            directoryMock.Setup(lib => lib.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>())).Returns(listFiles);

            container.Register(Component.For<IDirectory>().Instance(directoryMock.Object));
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

        [TestMethod]
        public void ExtractPathsUnitTest()
        {
            var container = new WindsorContainer();

            var directoryMock = new Mock<IDirectory>();
            var listFiles = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                listFiles.Add($"{Guid.NewGuid()}.cbr");
            }

            directoryMock.Setup(lib => lib.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>())).Returns(listFiles);


            var archiveMock = new Mock<IArchiveFactoryDecoupling>();
            List<string> list = new List<string>();
            list.Add("some_file.jpg");

            archiveMock.Setup(lib => lib.WriteToFiles(It.IsAny<string>())).Returns(list);
            archiveMock.Setup(lib => lib.FileName).Returns(fileName);

            container.Register(Component.For<IArchiveFactoryDecoupling>().Instance(archiveMock.Object));



            container.Register(Component.For<IDirectory>().Instance(directoryMock.Object));
            container.Register(Component.For<IExtractionRules>().ImplementedBy<ExtractionRules>());
            container.Register(Component.For<CbrPaths>().ImplementedBy<CbrPaths>());
            container.Register(Component.For<ICbrExtract>().ImplementedBy<CbrExtract>());



            var extractionRules = container.Resolve<IExtractionRules>();
            var extract = container.Resolve<ICbrExtract>();
            var paths = container.Resolve<CbrPaths>();
            paths.SourcePath = @"D:\Temp\pending";
            paths.SourcePath = @"D:\Temp\working";

            var output = extract.Extract(paths);

            Assert.IsTrue(extractionRules != null);
            Assert.IsTrue(extractionRules is ExtractionRules);
            Assert.IsTrue(extract != null);
            Assert.IsTrue(extract is CbrExtract);
            Assert.IsTrue(output != null);
            Assert.IsTrue(paths.SourcePath != null);
            Assert.IsTrue(paths.DestinationPath != null);
            Assert.IsTrue(output is List<CbrExtractOutput>);
            Assert.IsTrue(output.Count == 10);
        }


    }
}
