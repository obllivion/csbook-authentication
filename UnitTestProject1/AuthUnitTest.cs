using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using authentication;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class AuthUnitTest
    {

        public struct uPas
        {
            public string uname;
            public string pass;
        };

        [TestMethod]
        public void TestMethod1()
        {
            
            Startup st = new Startup();
            uPas car = new uPas();
            car.uname = "djolej@e2lfak.rs";
            car.pass = "Obllivion1!";
            Task<object> t = st.Invoke(car);
            //t.Wait();
            bool res = (bool)t.Result;
        }
    }
}
