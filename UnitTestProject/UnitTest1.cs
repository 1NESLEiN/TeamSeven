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
        }
    }
}
