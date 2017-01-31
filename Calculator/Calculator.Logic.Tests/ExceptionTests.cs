using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ExceptionTests
    {
        class Used
        {
            public void SubDo2()
            {
                throw new ArgumentException();
            }
        }
        class Callee
        {
            public void Do()
            {
                try
                {
                    SubDo();
                    new Used().SubDo2();
                }
                catch (ArgumentException x) {
                    OnError(x.Message);
                }
            }
            public void SubDo()
            {
                throw new ArgumentException();
            }
            public Action<string> OnError { get; set; }

        }

        public class MyException : Exception
        {
            public MyException(string error, int index) : base(error)
            {
                Index = index;
            }
            public int Index { get; }
        }
        [Test]
        public void Catch()
        {
            var callee = new Callee();
            callee.OnError = Console.WriteLine;
            try
            {
                callee.Do();
            }
            catch (ArgumentException x) { }
            
        }
    }
}
