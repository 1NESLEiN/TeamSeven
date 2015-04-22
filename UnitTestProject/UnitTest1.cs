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
            TestHandler handler = TestHandler.GetInstance();

            string headline = "Headline";
            string description = "Description";
            int typeId = 1;
            int supporterId = 1;
            DateTime? dateCompleted = DateTime.Parse("2015-04-23 08:30:00");
            int timeSpent = 20;
            DateTime dateCreated = DateTime.Parse("2015-04-22 20:43:23");
            int statusId = 3;

            Documentation testDocumentation1 = new Documentation(headline, description, typeId, supporterId, dateCompleted, timeSpent, dateCreated, statusId);
            handler.AddDocumentation(headline, description, typeId, supporterId, dateCompleted, timeSpent, dateCreated, statusId);

            headline = "Overskrift";
            description = "Beskrivelse";
            typeId = 2;
            supporterId = 2;
            dateCompleted = null;
            timeSpent = 0;
            dateCreated = DateTime.Parse("2015-04-22 20:43:23");
            statusId = 1;

            Documentation testDocumentation2 = new Documentation(headline, description, typeId, supporterId, dateCompleted, timeSpent, dateCreated, statusId);
            handler.AddDocumentation(headline, description, typeId, supporterId, dateCompleted, timeSpent, dateCreated, statusId);

           //Test GetDocumentation method
            Documentation saveDocumentation1 = new Documentation(handler.GetDocumentation(1).Rows[0]);
            Documentation saveDocumentation2 = new Documentation(handler.GetDocumentation(2).Rows[0]);

            //Test that the equal method can determine if two Documentation objects have the same values
            Assert.IsTrue(testDocumentation1.Equals(saveDocumentation1));
            Assert.IsTrue(testDocumentation2.Equals(saveDocumentation2));
            Assert.IsFalse(testDocumentation2.Equals(saveDocumentation1));
            Assert.IsFalse(testDocumentation1.Equals(saveDocumentation2));
        }

        [TestMethod]
        public void TestSearchDocumentation()
        {
            
        }
    }
}
