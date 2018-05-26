using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INC.EntityFramework.Test
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext _context;

        [TestInitialize]
        public void Initializer()
        {
            _context = new TestContext();
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (var uow = new UnitOfWork(_context))
            {
                var resp = uow.Repositories<Student>();
                var list = resp.All(x=>1==1, "FirstName desc",0,10);

                resp.AddRange(new Student()
                {
                    FirstName = "a",
                    LastName = "b"
                }, new Student()
                {
                    FirstName = "c",
                    LastName = "d"
                });

                uow.SaveChanges();

                var list1 = resp.All();
                Assert.AreEqual(list1.Count, 1);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            using (var uow = new UnitOfWork(_context))
            {
                var resp = uow.Repositories<Student>();
                var list1 = resp.All();

                resp.RemoveRange(list1);
                uow.SaveChanges();

                Assert.AreEqual(list1.Count, 1);
            }
        }
    }
}
