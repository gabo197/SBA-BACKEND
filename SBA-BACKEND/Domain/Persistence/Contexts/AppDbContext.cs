using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Specialty> Specialties{ get; set; }
        public DbSet<TechnicianSpecialty> TechnicianSpecialties { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //ENTITIES

            //builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Address>().ToTable("Address");
            builder.Entity<Opinion>().ToTable("Opinions");
            builder.Entity<Report>().ToTable("Reports");
            builder.Entity<Specialty>().ToTable("Specialties");
            builder.Entity<TechnicianSpecialty>().ToTable("SpecialtyTechnicians");
            //builder.Entity<Technician>().ToTable("Technicians");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Profile>().ToTable("Profiles");

            //CONSTRAINTS

            //Constraints of User
            builder.Entity<User>().HasKey(user => user.Id); //Primary Key
            builder.Entity<User>().Property(user => user.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<User>().Property(user => user.Email).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(user => user.Password).IsRequired().HasMaxLength(150);

            //Constraints of Profile
            builder.Entity<Profile>().HasKey(p => p.UserId);
            builder.Entity<Profile>().HasDiscriminator<string>("user_type")
                .HasValue<Customer>("customer")
                .HasValue<Technician>("technician");
            builder.Entity<Profile>().Property(p => p.UserId).IsRequired();
            builder.Entity<Profile>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Profile>().Property(p => p.LastName).IsRequired();
            builder.Entity<Profile>().Property(p => p.PhoneNumber).IsRequired();

            //Constraints of Customer
            //builder.Entity<Customer>().Property(customer => customer.FirstName).IsRequired().HasMaxLength(50);
            //builder.Entity<Customer>().Property(customer => customer.LastName).IsRequired().HasMaxLength(50);
            //builder.Entity<Customer>().Property(customer => customer.ImageUrl).IsRequired().HasMaxLength(50);
            //builder.Entity<Customer>().Property(customer => customer.Description).IsRequired().HasMaxLength(150);
            //builder.Entity<Customer>().Property(customer => customer.PhoneNumber).IsRequired();

            //Constraints of Technician
            //builder.Entity<Technician>().Property(technician => technician.FirstName).IsRequired().HasMaxLength(50);
            //builder.Entity<Technician>().Property(technician => technician.LastName).IsRequired().HasMaxLength(50);
            //builder.Entity<Technician>().Property(technician => technician.ImageUrl).IsRequired().HasMaxLength(150);
            //builder.Entity<Technician>().Property(technician => technician.Description).IsRequired();
            //builder.Entity<Technician>().Property(technician => technician.PhoneNumber).HasMaxLength(300);

            //Constraints of Address
            builder.Entity<Address>().HasKey(address => address.UserId); //Primary Key
            builder.Entity<Address>().Property(address => address.Region).IsRequired().HasMaxLength(50);
            builder.Entity<Address>().Property(address => address.Province).IsRequired().HasMaxLength(50);
            builder.Entity<Address>().Property(address => address.District).IsRequired().HasMaxLength(50);
            builder.Entity<Address>().Property(address => address.FullAddress).IsRequired().HasMaxLength(100);

            //Constraints of Opinion
            builder.Entity<Opinion>().HasKey(opinion => opinion.Id); //Primary Key
            builder.Entity<Opinion>().Property(opinion => opinion.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<Opinion>().Property(opinion => opinion.Stars).IsRequired();
            builder.Entity<Opinion>().Property(opinion => opinion.Description).IsRequired().HasMaxLength(300);

            //Constraints of Report
            builder.Entity<Report>().HasKey(report => report.Id); //Primary Key
            builder.Entity<Report>().Property(report => report.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<Report>().Property(report => report.Description).IsRequired().HasMaxLength(300);

            //Constraints of Specialty
            builder.Entity<Specialty>().HasKey(specialty => specialty.Id); //Primary Key
            builder.Entity<Specialty>().Property(specialty => specialty.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<Specialty>().Property(specialty => specialty.Name).IsRequired().HasMaxLength(30);

            //Constraints of SpecialtyTechnician
            builder.Entity<TechnicianSpecialty>().HasKey(st => new { st.TechnicianId, st.SpecialtyId }); //Primary Key

            //RELATIONSHIPS

            //Relationships of User

            builder.Entity<User>() //One to One with Profile
              .HasOne(user => user.Profile)
              .WithOne(profile => profile.User)
              .HasForeignKey<Profile>(profile => profile.UserId);

            builder.Entity<User>() //One to One with Address
                .HasOne(user => user.Address)
                .WithOne(address => address.User)
                .HasForeignKey<Address>(address => address.UserId);

            //Relationships of Opinion

            builder.Entity<Opinion>() //One to Many with Customer
                .HasOne(opinion => opinion.Customer)
                .WithMany(customer => customer.Opinions)
                .HasForeignKey(opinion => opinion.CustomerId);

            builder.Entity<Opinion>() //One to Many with Technician
                .HasOne(opinion => opinion.Technician)
                .WithMany(technician => technician.Opinions)
                .HasForeignKey(opinion => opinion.TechnicianId);

            //Relationships of Report

            builder.Entity<Report>() //One to Many with Customer
                .HasOne(report => report.Customer)
                .WithMany(customer => customer.Reports)
                .HasForeignKey(report => report.CustomerId);

            builder.Entity<Report>() //One to Many with Technician
                .HasOne(report => report.Technician)
                .WithMany(technician => technician.Reports)
                .HasForeignKey(report => report.TechnicianId);

            //Relationships of SpecialtyTechnician

            //Many to Many with Specialty and Technician
            builder.Entity<TechnicianSpecialty>()
                .HasOne(st => st.Specialty)
                .WithMany(specialty => specialty.TechnicianSpecialties)
                .HasForeignKey(st => st.SpecialtyId);
            builder.Entity<TechnicianSpecialty>()
                .HasOne(st => st.Technician)
                .WithMany(technician => technician.TechnicianSpecialties)
                .HasForeignKey(st => st.TechnicianId);

            //SEED DATA

            builder.Entity<User>().HasData
                (
                    new User
                    {
                        Id = 100,
                        Email = "pedro.mustafa@gmail.com",
                        Password = "mu574f4"
                    },
                    new User
                    {
                        Id = 101,
                        Email = "leopoldo.murcia@gmail.com",
                        Password = "murc14"
                    },
                    new User
                    {
                        Id = 102,
                        Email = "beatriz.romero@gmail.com",
                        Password = "r0m3r0"
                    },
                    new User
                    {
                        Id = 103,
                        Email = "lucy.flores@gmail.com",
                        Password = "fl0r3s"
                    },
                    new User
                    {
                        Id = 104,
                        Email = "xabier.diaz@gmail.com",
                        Password = "d14z"
                    },
                    new User
                    {
                        Id = 105,
                        Email = "valeriano.cuellar@gmail.com",
                        Password = "cu3ll4r"
                    },
                    new User
                    {
                        Id = 106,
                        Email = "marta.tapia@gmail.com",
                        Password = "74p15"
                    }
                );

            builder.Entity<Customer>().HasData
                (
                    new Customer
                    {
                        UserId = 100,
                        FirstName = "Pedro",
                        LastName = "Mustafa",
                        PhoneNumber = "993724956",
                        ImageUrl = "https://wl-genial.cf.tsp.li/resize/728x/jpg/f6e/ef6/b5b68253409b796f61f6ecd1f1.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 101,
                        FirstName = "Leopoldo",
                        LastName = "Murcia",
                        PhoneNumber = "993724957",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSOFL8AwuGJlfjJrd6QZskD9LSbu5hqNRghaGpl_rqQGPcykkB6Pj3QKlzoEMN2_jtZzO8&usqp=CAU",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 102,
                        FirstName = "Beatriz",
                        LastName = "Romero",
                        PhoneNumber = "993724958",
                        ImageUrl = "https://i.pinimg.com/736x/c3/c7/ce/c3c7ce0b340ef81623a2b391ead722c9.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 103,
                        FirstName = "Luciana",
                        LastName = "Flores",
                        PhoneNumber = "993724959",
                        ImageUrl = "https://i.pinimg.com/474x/c0/cd/df/c0cddff8bce605d57a003ae1d98026ce.jpg",
                        Description = ""
                    }
                );

            builder.Entity<Address>().HasData
                (
                    new Address
                    {
                        UserId = 100,
                        Region = "Lima",
                        Province = "Lima",
                        District = "Miraflores",
                        FullAddress = "Calle Francia 523"
                    },
                    new Address
                    {
                        UserId = 101,
                        Region = "Lima",
                        Province = "Lima",
                        District = "San Isidro",
                        FullAddress = "Calle Olaechea 147"
                    },
                    new Address
                    {
                        UserId = 102,
                        Region = "Lima",
                        Province = "Lima",
                        District = "San Borja",
                        FullAddress = "Calle Puccini 235"
                    },
                    new Address
                    {
                        UserId = 103,
                        Region = "Lima",
                        Province = "Lima",
                        District = "Surco",
                        FullAddress = "Av. La Encalada 454"
                    },
                    new Address
                    {
                        UserId = 104,
                        Region = "Lima",
                        Province = "Lima",
                        District = "San Borja",
                        FullAddress = "Calle de Las Letras 245"
                    },
                    new Address
                    {
                        UserId = 105,
                        Region = "Lima",
                        Province = "Lima",
                        District = "Surco",
                        FullAddress = "Calle Domingo Nieto 342"
                    },
                    new Address
                    {
                        UserId = 106,
                        Region = "Lima",
                        Province = "Lima",
                        District = "San Isidro",
                        FullAddress = "Calle Las Flores 459"
                    }
                );

            builder.Entity<Opinion>().HasData
                (
                    new Opinion
                    {
                        Id = 100,
                        CustomerId = 100,
                        TechnicianId = 104,
                        Description = "excelente servicio",
                        Stars = 5
                    },
                    new Opinion
                    {
                        Id = 101,
                        CustomerId = 101,
                        TechnicianId = 105,
                        Description = "excelente servicio",
                        Stars = 5
                    },
                    new Opinion
                    {
                        Id = 102,
                        CustomerId = 102,
                        TechnicianId = 106,
                        Description = "excelente servicio",
                        Stars = 5
                    }
                );

            builder.Entity<Report>().HasData
                (
                    new Report
                    {
                        Id = 100,
                        CustomerId = 103,
                        TechnicianId = 105,
                        Description = "acoso sexual"
                    }
                );

            builder.Entity<Specialty>().HasData
                (
                    new Specialty
                    {
                        Id = 100,
                        Name = "Gasfitero"
                    },
                    new Specialty
                    {
                        Id = 101,
                        Name = "Electricista"
                    },
                    new Specialty
                    {
                        Id = 102,
                        Name = "Jardinero"
                    }
                );

            builder.Entity<Technician>().HasData
                (
                    new Technician
                    {
                        UserId = 104,
                        FirstName = "Xabier",
                        LastName = "Díaz",
                        PhoneNumber = "926503728",
                        ImageUrl = "https://scontent.flim18-2.fna.fbcdn.net/v/t1.6435-9/67204430_1735072573458413_4218327998354423808_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=CVIMPaSZ98UAX9IKXV8&_nc_ht=scontent.flim18-2.fna&oh=69d3a868d4c40cd653aad03fd9dc9050&oe=617F9A17",
                        Description = "Gasfitero con 10 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 105,
                        FirstName = "Valeriano",
                        LastName = "Cuellar",
                        PhoneNumber = "926503721",
                        ImageUrl = "https://i.pinimg.com/originals/79/2f/3e/792f3efaa465b09469e0b1b6b520daa6.jpg",
                        Description = "Electricista con 10 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 106,
                        FirstName = "Marta",
                        LastName = "Tapia",
                        PhoneNumber = "926503728",
                        ImageUrl = "https://www.okchicas.com/wp-content/uploads/2018/01/Poses-para-una-buena-foto-de-perfil-11.jpg",
                        Description = "Jardinera con 10 años de experiencia"
                    }
                );
        }
    }
}
