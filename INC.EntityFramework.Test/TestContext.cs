using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace INC.EntityFramework.Test
{
    public class TestContext : DbContext
    {
        public TestContext():base("mssqllocaldb.db")
        {
        }

        public DbSet<Student> Student { get; set; }
    }

    public class Student
    {
        [Key]
        public long id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
