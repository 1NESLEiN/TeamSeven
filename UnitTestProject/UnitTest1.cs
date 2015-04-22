using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddDocumentation()
        {
            TestDbHandler dbHandler = TestDbHandler.GetInstance();
            dbHandler.BuildDatabase();

            Documentation testDocumentation1 = new Documentation("Headline", "Description", 1, 1, DateTime.Parse("2015-04-23 08:30:00"), 20, DateTime.Parse("2015-04-22 20:43:23"), 3);
            Documentation testDocumentation2 = new Documentation("Overskrift", "Beskrivelse", 2, 2, null, 0, DateTime.Parse("2015-04-22 20:43:23"), 1);

            
            //Test AddDocumentation method
            dbHandler.AddDocumentation(testDocumentation1);
            dbHandler.AddDocumentation(testDocumentation2);

            //Test GetDocumentation method
            Documentation saveDocumentation1 = new Documentation(dbHandler.GetDocumentation(1).Rows[0]);
            Documentation saveDocumentation2 = new Documentation(dbHandler.GetDocumentation(2).Rows[0]);

            //Test that the equal method can determine if two Documentation objects have the same values
            Assert.IsTrue(testDocumentation1.Equals(saveDocumentation1));
            Assert.IsTrue(testDocumentation2.Equals(saveDocumentation2));
            Assert.IsFalse(testDocumentation2.Equals(saveDocumentation1));
            Assert.IsFalse(testDocumentation1.Equals(saveDocumentation2));
        }

        
    }
}
