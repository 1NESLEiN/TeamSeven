using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control;

namespace UnitTestProject
{
    public class TestHandler : Handler
    {
        private static TestDbHandler _testDbHandler;
        private static TestHandler _testHandler;

        private TestHandler()
        {
            _dbHandler = TestDbHandler.GetInstance();
            _testDbHandler = TestDbHandler.GetInstance();
            _testDbHandler.BuildDatabase();
        }

        public static TestHandler GetInstance()
        {
            if (_testHandler == null)
            {
                _testHandler = new TestHandler();
            }
            return _testHandler;
        }
    }
}
