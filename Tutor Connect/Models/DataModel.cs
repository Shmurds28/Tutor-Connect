using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Tutor_Connect.Models
{
    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<TutorModule> TutorModules { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.staffNumber)
                .IsFixedLength();

            modelBuilder.Entity<Admin>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Applicant>()
                .Property(e => e.studNumber)
                .IsFixedLength();

            modelBuilder.Entity<Applicant>()
                .Property(e => e.applicationDate)
                .IsFixedLength();

            modelBuilder.Entity<Applicant>()
                .Property(e => e.moduleCode)
                .IsFixedLength();

            modelBuilder.Entity<Booking>()
                .Property(e => e.studNumber)
                .IsFixedLength();

            modelBuilder.Entity<Module>()
                .Property(e => e.moduleCode)
                .IsFixedLength();

            modelBuilder.Entity<Review>()
                .Property(e => e.studNumber)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.StudNumber)
                .IsFixedLength();


            modelBuilder.Entity<TutorModule>()
                .Property(e => e.studNumber)
                .IsFixedLength();

            modelBuilder.Entity<TutorModule>()
                .Property(e => e.moduleCode)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.type)
                .IsFixedLength();
        }
    }
}
