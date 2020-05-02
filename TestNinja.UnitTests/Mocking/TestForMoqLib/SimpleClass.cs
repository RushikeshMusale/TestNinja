using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests.Mocking.TestForMoqLib
{
   public  class SimpleClass
    {
        public SimpleClass(string parameter)
        {

        }

        // it does need default constructor to create a mock object
        public SimpleClass()
        {

        }

        // Only methods abstract or virtual can be mocked
        public virtual string  ReturnsHello()
        {
            return "hello";
        }
    }
}
