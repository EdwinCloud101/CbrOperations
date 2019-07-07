using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CbrOperations;
using DotNetDecoupling.System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CbrOperationsUnitTests
{
    [TestClass]
    public class InputTests
    {

        public void OneCbrPath1Test()
        {
            var container = new WindsorContainer();

            string sourceFolder = "folderA";
            string destinationFolder = "folderB";


            container.Register(Component.For<CbrPaths>().ImplementedBy<CbrPaths>().DependsOn(Dependency.OnValue<string>(sourceFolder)).DependsOn(Dependency.OnValue<string>(sourceFolder)));
            var onePath = container.Resolve<CbrPaths>();

            Assert.IsTrue(onePath != null);
            Assert.IsTrue(onePath is CbrPaths);
            Assert.IsTrue(sourceFolder == onePath.SourcePath);
            Assert.IsTrue(destinationFolder == onePath.DestinationPath);
        }



    }
}
