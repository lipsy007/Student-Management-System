using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ishant_Goyal.Models
{
    public partial class IshantContext : DbContext
    {
        public IshantContext()
        {
        }

        public IshantContext(DbContextOptions<IshantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseAssignment> CourseAssignment { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-HDB2E58S;Database=Ishant;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).ValueGeneratedOnAdd();

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.StateCodeNavigation)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateCode)
                    .HasConstraintName("FK_City_state");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseAssignment>(entity =>
            {
                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseAssignment)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseAssignment_Course");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseAssignment)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseAssignment_Instructor");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EnrollmentDate).HasColumnType("date");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.InstructorId).ValueGeneratedNever();

                entity.Property(e => e.InstructorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate).HasColumnType("date");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateCode);

                entity.ToTable("state");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).ValueGeneratedOnAdd();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.CityCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnrollmentDate).HasColumnType("date");

                entity.Property(e => e.Fees).HasColumnType("money");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.CityCode)
                    .HasConstraintName("FK_Student_City");

                entity.HasOne(d => d.StateCodeNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.StateCode)
                    .HasConstraintName("FK_Student_state");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivationCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
