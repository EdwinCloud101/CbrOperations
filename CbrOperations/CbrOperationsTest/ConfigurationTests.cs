using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CbrOperations;
using CbrOperations.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CbrOperationsUnitTests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ExtractionRulesTest1()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IExtractionRules>().ImplementedBy<ExtractionRules>());
            container.Register(Component.For<ICbrExtract>().ImplementedBy<CbrExtract>());

            var extractionRules = container.Resolve<IExtractionRules>();
            var extract = container.Resolve<ICbrExtract>();

            Assert.IsTrue(extractionRules != null);
            Assert.IsTrue(extractionRules is ExtractionRules);
            Assert.IsTrue(extract != null);
            Assert.IsTrue(extract is CbrExtract);
        }
    }
}
