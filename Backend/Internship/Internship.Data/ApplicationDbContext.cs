using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;
using Internship.Data.DomainClasses;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Internship.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount>
    {
        public DbSet<Stagevoorstel> Stagevoorstellen { get; set; }
        public DbSet<Stageopdracht> Stageopdrachten { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public DbSet<Contactpersoon> Contactpersonen { get; set; }
        public DbSet<Bedrijfspromotor> Bedrijfspromotors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Student> Studenten { get; set; }
        public DbSet<Lector> Lectoren { get; set; }
        public DbSet<Stagecoördinator> Stagecoördinators { get; set; }
        public DbSet<Stage> Stages { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //NAMING TABLES
            modelBuilder.Entity<Stagevoorstel>().ToTable("Stagevoorstel");
            modelBuilder.Entity<Stageopdracht>().ToTable("Stageopdracht");
            modelBuilder.Entity<Bedrijf>().ToTable("Bedrijf");
            modelBuilder.Entity<Contactpersoon>().ToTable("Contactpersoon");
            modelBuilder.Entity<Bedrijfspromotor>().ToTable("Bedrijfpromotor");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Lector>().ToTable("Lector");
            modelBuilder.Entity<Stagecoördinator>().ToTable("Stagecoördinaor");
            modelBuilder.Entity<Stage>().ToTable("Stage");

            //RELATIONS
            modelBuilder.Entity<Stagevoorstel>()
                .HasRequired<Stageopdracht>(s => s.Stageopdracht)
                .WithRequiredDependent();
            modelBuilder.Entity<Stagevoorstel>()
                .HasRequired<Bedrijf>(s => s.Opdrachtgever)
                .WithMany(b => b.IngediendeStagevoorstellen);
            modelBuilder.Entity<Stageopdracht>()
                .HasRequired<Contactpersoon>(s => s.Contactpersoon)
                .WithMany(c => c.Stageopdrachten);
            modelBuilder.Entity<Stageopdracht>()
                .HasRequired<Bedrijfspromotor>(s => s.Bedrijfspromotor)
                .WithMany(b => b.Stageopdrachten);
            modelBuilder.Entity<Stagevoorstel>().HasOptional(s => s.Review).WithRequired(s => s.Stagevoorstel);
            modelBuilder.Entity<Lector>().HasMany(r => r.Reviews).WithRequired(r => r.Reviewer);
            modelBuilder.Entity<Stageopdracht>().HasOptional(s => s.Stage).WithRequired(s => s.StageOpdracht);
            

            modelBuilder.Entity<Lector>().HasMany(l => l.Stagevoorstellen).WithOptional(l => l.ReviewLector);

            modelBuilder.Entity<Bedrijf>().HasOptional(b => b.UserAccount).WithMany();
            modelBuilder.Entity<Lector>().HasOptional(b => b.UserAccount).WithMany();
            modelBuilder.Entity<Stagecoördinator>().HasOptional(b => b.UserAccount).WithMany();
            modelBuilder.Entity<Student>().HasOptional(b => b.UserAccount).WithMany();
            modelBuilder.Entity<Student>().HasOptional(b => b.GekozenStageOpdracht).WithRequired(b => b.Student);

            modelBuilder.Entity<Stagecoördinator>()
                .HasMany(s => s.ToeTeKennenStagevoorstellen)
                .WithOptional(s => s.StagecoördinatorBehandelingLector);
            modelBuilder.Entity<Stagecoördinator>()
                .HasMany(s => s.AanvragingenStageopdrachtenStudent)
                .WithOptional(s => s.StagecoördinatorBehandelingStageopdrachtStudent);

            modelBuilder.Entity<Student>().HasMany(s => s.Favorieten).WithMany(s => s.StudentFavorieten).Map(s => s.ToTable("StudentFavoriet"));

            modelBuilder.Entity<Bedrijf>().HasMany(b => b.Bedrijfspromotors).WithRequired(p => p.BedrijfInDienst).WillCascadeOnDelete(false); ;
            modelBuilder.Entity<Bedrijf>().HasMany(b => b.Contactpersonen).WithRequired(b => b.BedrijfInDienst).WillCascadeOnDelete(false); ;

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
