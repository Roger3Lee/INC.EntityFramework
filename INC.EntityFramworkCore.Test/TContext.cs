using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace INC.EntityFrameworkCore.Test
{
    public class TContext : DbContext
    {
        public TContext():base()
        {
        }
        public TContext(DbContextOptions<TContext> options)
        : base(options)
        { }
        public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blog.db", x => x.MigrationsAssembly("INC.EntityFramworkCore.Test"));
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class Student
    {
        [Key]
        public long id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IList<Teacher> Teacher { get; set; }
    }

    public class Teacher
    {
        public long id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
