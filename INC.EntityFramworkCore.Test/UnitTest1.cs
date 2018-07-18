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
                

                uow.SaveChanges();

                var list1 = resp.All();
                Assert.AreEqual(list1.Count(), 1);
            }
        }

        [TestMethod]
        public void TestInclude()
        {
            using (var uow = new UnitOfWork(_context))
            {
                var resp = uow.Repositories<Student>();
                var list1 = resp.Include(x=>x.Teacher).All();
                
                Assert.AreEqual(list1.Count(), 1);
            }
        }

        [TestMethod]
        public void TestMutiThreadInclude()
        {
            var task1 = new System.Threading.Tasks.Task(() =>
            {
                System.Threading.Thread.Sleep(10000);
                using (var uow = new UnitOfWork(new TContext()))
                {
                    var resp = uow.Repositories<Student>();
                    var list1 = resp.Include(x => x.Teacher).All();
                }
            });

           var task2= new System.Threading.Tasks.Task(() =>
            {
                using (var uow = new UnitOfWork(new TContext()))
                {
                    var resp = uow.Repositories<Student>();
                    resp.Include(x => x.Teacher) ;


                    System.Threading.Thread.Sleep(100000);
                    resp.All();
                }
            });

            task1.Start();
            task2.Start();
            System.Threading.Tasks.Task.WaitAll(task1, task2);
        }
    }
}
