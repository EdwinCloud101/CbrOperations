using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CbrOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CbrOperationsUnitTests
{
    [TestClass]
    public class InputTests
    {
        [TestMethod]
        public void OneCbrFile1Test()
        {
            var container = new WindsorContainer();

            string fileName = @"somefile.cbr";
            container.Register(Component.For<IOneCbrFile>().ImplementedBy<OneCbrFile>().DependsOn(Dependency.OnValue<string>(fileName)));
            var oneFile = container.Resolve<IOneCbrFile>();

            Assert.IsTrue(oneFile != null);
            Assert.IsTrue(oneFile is OneCbrFile);
            Assert.IsTrue(fileName == oneFile.FileName);
        }


        public void OneCbrPath1Test()
        {
            var container = new WindsorContainer();

            string sourceFolder = "folderA";
            string destinationFolder = "folderB";


            container.Register(Component.For<ICbrPaths>().ImplementedBy<CbrPaths>().DependsOn(Dependency.OnValue<string>(sourceFolder)).DependsOn(Dependency.OnValue<string>(sourceFolder)));
            var onePath = container.Resolve<ICbrPaths>();

            Assert.IsTrue(onePath != null);
            Assert.IsTrue(onePath is CbrPaths);
            Assert.IsTrue(sourceFolder == onePath.SourcePath);
            Assert.IsTrue(destinationFolder == onePath.DestinationPath);
        }






        [TestMethod]
        public void BasicExtractNewing()
        {
            //string fileName = @"somefile.cbr";
            //ICbrInput input = new CbrInput(fileName, true, false);
            //ICbrExtract extract = new CbrExtract();
            //ICbrExtractOutput output = extract.Extract(input);


            //Assert.IsTrue(input != null);
            //Assert.IsTrue(input.Path == input.SourcePath);
            //Assert.IsTrue(System.IO.Path.GetExtension(input.Path) == ".cbr");
            //Assert.IsTrue(output != null);
        }
    }
}
