using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestDbHandler dbHandler = TestDbHandler.GetInstance();
            dbHandler.BuildDatabase();

            Documentation testDocumentation = new Documentation("Headline", "Description", 1, 1, null, 20, DateTime.Now, 1);

            
            //Test AddDocumentation method
            dbHandler.AddDocumentation(testDocumentation);

            //Test GetDocumentation method
            Documentation saveDocumentation = new Documentation(dbHandler.GetDocumentation(1).Rows[0]);

            Assert.IsTrue(testDocumentation.Equals(saveDocumentation));
        }
    }
}
