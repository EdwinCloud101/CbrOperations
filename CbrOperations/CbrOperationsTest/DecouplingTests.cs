using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CbrOperations.Decoupling.SharpCompress;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CbrOperationsUnitTests
{
    [TestClass]
    public class DecouplingTests
    {
        [TestMethod]
        public void ArchiveFactoryDecouplingTest1()
        {
            var container = new WindsorContainer();



            container.Register(Component.For<IArchiveFactoryDecoupling>().ImplementedBy<ArchiveFactoryDecoupling>().DependsOn(Dependency.OnValue<string>(@"some file")));

            var acArchiveFactoryDecoupling = container.Resolve<IArchiveFactoryDecoupling>();
            var entries = acArchiveFactoryDecoupling.OpenAndGetEntries();

            Assert.IsTrue(entries != null && entries.ToList().Count == 0);
        }
    }
}
