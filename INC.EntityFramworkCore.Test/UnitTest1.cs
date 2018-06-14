using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace INC.EntityFrameworkCore.Test
{
    [TestClass]
    public class UnitTest1
    {
        private TContext _context;

        [TestInitialize]
        public void Initializer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");
            _context = new TContext(optionsBuilder.Options);
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
                Assert.AreEqual(list1.Count(), 1);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            using (var uow = new UnitOfWork(_context))
            {
                var resp = uow.Repositories<Student>();
                var list1 = resp.Include(x=>x.Teacher).All();
                
                Assert.AreEqual(list1.Count(), 1);
            }
        }
    }
}
