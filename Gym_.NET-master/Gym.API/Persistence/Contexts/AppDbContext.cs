using System;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;

namespace Gym.API.Persistence.Contexts {
    public class AppDbContext : DbContext {
        public DbSet<AdministratorHall> AdministratorHall { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<CardSalesHistory> CardSalesHistory { get; set; }
        public DbSet<ClassRecords> ClassRecords { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<ScheduleTraining> ScheduleTraining { get; set; }
        public DbSet<ServicesCard> ServicesCard { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region AdministratorHall

            builder.Entity<AdministratorHall>().ToTable("AdministratorHall");
            builder.Entity<AdministratorHall>().HasKey(x => x.IdAdministrator);
            builder.Entity<AdministratorHall>().Property(x => x.IdAdministrator).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AdministratorHall>().Property(x => x.Status).IsRequired().HasMaxLength(150);

            #endregion

            #region News

            builder.Entity<News>().ToTable("News");
            builder.Entity<News>().HasKey(x => x.IdNews);
            builder.Entity<News>().Property(x => x.IdNews).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<News>().Property(x => x.Title).IsRequired();
            builder.Entity<News>().Property(x => x.Text).IsRequired();
            builder.Entity<News>().Property(x => x.Date).IsRequired();

            #endregion

            #region Card

            builder.Entity<Card>().ToTable("Card");
            builder.Entity<Card>().HasKey(x => x.IdCard);
            builder.Entity<Card>().Property(x => x.IdCard).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Card>().Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Entity<Card>().Property(x => x.Period).IsRequired().HasMaxLength(150);
            builder.Entity<Card>().Property(x => x.Image).IsRequired().HasMaxLength(150);
            builder.Entity<Card>().Property(x => x.Price).IsRequired().HasColumnType("decimal(5,2)");

            #endregion

            #region CardSalesHistory

            builder.Entity<CardSalesHistory>().ToTable("CardSalesHistory");
            builder.Entity<CardSalesHistory>().HasKey(x => x.IdSalesHistory);
            builder.Entity<CardSalesHistory>().Property(x => x.IdSalesHistory).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<CardSalesHistory>().Property(x => x.IdUser).IsRequired();
            builder.Entity<CardSalesHistory>().Property(x => x.IdCard).IsRequired();
            builder.Entity<CardSalesHistory>().Property(x => x.Date).IsRequired().HasColumnType("Date");

            #endregion

            #region ClassRecords

            builder.Entity<ClassRecords>().ToTable("ClassRecords");
            builder.Entity<ClassRecords>().HasKey(x => x.IdClassRecords);
            builder.Entity<ClassRecords>().Property(x => x.IdClassRecords).IsRequired().ValueGeneratedOnAdd();

            #endregion

            #region Client

            builder.Entity<Client>().ToTable("Client");
            builder.Entity<Client>().HasKey(x => x.IdClient);
            builder.Entity<Client>().Property(x => x.IdClient).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Client>().Property(x => x.Status).IsRequired().HasMaxLength(150);
            builder.Entity<Client>().HasMany(x => x.ClassRecords).WithOne(x => x.Client);

            #endregion

            #region Role

            builder.Entity<Role>().ToTable("Role");
            builder.Entity<Role>().HasKey(x => x.IdRole);
            builder.Entity<Role>().Property(x => x.IdRole).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Entity<Role>().HasMany(x => x.UserRole).WithOne(x => x.Role);

            #endregion

            #region Room

            builder.Entity<Room>().ToTable("Room");
            builder.Entity<Room>().HasKey(x => x.IdRoom);
            builder.Entity<Room>().Property(x => x.IdRoom).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Room>().Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Entity<Room>().HasMany(x => x.ScheduleTraining).WithOne(x => x.Room);

            #endregion

            #region ScheduleTraining

            builder.Entity<ScheduleTraining>().ToTable("ScheduleTraining");
            builder.Entity<ScheduleTraining>().HasKey(x => x.IdTraining);
            builder.Entity<ScheduleTraining>().Property(x => x.IdTraining).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ScheduleTraining>().Property(x => x.Type).IsRequired().HasMaxLength(150);
            builder.Entity<ScheduleTraining>().Property(x => x.TrainingDateFrom).IsRequired();
            builder.Entity<ScheduleTraining>().Property(x => x.IdRoom).IsRequired();
            builder.Entity<ScheduleTraining>().Property(x => x.IdTrainer).IsRequired();
            builder.Entity<ScheduleTraining>().Property(x => x.TrainingDateTo).IsRequired();
            builder.Entity<ScheduleTraining>().Property(x => x.Engaged).IsRequired();
            builder.Entity<ScheduleTraining>().Property(x => x.RecordIsClosed).IsRequired();
            builder.Entity<ScheduleTraining>().HasMany(x => x.ClassRecords).WithOne(x => x.ScheduleTraining);

            #endregion

            #region ServicesCard

            builder.Entity<ServicesCard>().ToTable("ServicesCard");
            builder.Entity<ServicesCard>().HasKey(x => x.IdServices);
            builder.Entity<ServicesCard>().Property(x => x.IdServices).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ServicesCard>().Property(x => x.Name).IsRequired().HasMaxLength(150);

            #endregion

            #region Specialization

            builder.Entity<Specialization>().ToTable("Specialization");
            builder.Entity<Specialization>().HasKey(x => x.IdSpecialization);
            builder.Entity<Specialization>().Property(x => x.IdSpecialization).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Specialization>().Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Entity<Specialization>().HasMany(x => x.ScheduleTraining).WithOne(x => x.Specialization);

            #endregion

            #region Trainer

            builder.Entity<Trainer>().ToTable("Trainer");
            builder.Entity<Trainer>().HasKey(x => x.IdTrainer);
            builder.Entity<Trainer>().Property(x => x.IdTrainer).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Trainer>().Property(x => x.Status).IsRequired().HasMaxLength(150);
            builder.Entity<Trainer>().HasMany(x => x.ScheduleTraining).WithOne(x => x.Trainer);

            #endregion

            #region User

            builder.Entity<User>().ToTable("User");
            builder.Entity<User>().HasKey(x => x.IdUser);
            builder.Entity<User>().Property(x => x.IdUser).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Phone).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Birthday).IsRequired();
            builder.Entity<User>().Property(x => x.FullName).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Gender).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(x => x.Token);
            builder.Entity<User>().HasMany(x => x.AdministratorHall).WithOne(x => x.User);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);

            #endregion

            #region UserRole

            builder.Entity<UserRole>().ToTable("UserRole");
            builder.Entity<UserRole>().HasKey(x => x.IdUserRole);
            builder.Entity<UserRole>().Property(x => x.IdUserRole).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserRole>().Property(x => x.IdUser).IsRequired();
            builder.Entity<UserRole>().Property(x => x.IdRole).IsRequired();
            
            #endregion
        }
    }
}