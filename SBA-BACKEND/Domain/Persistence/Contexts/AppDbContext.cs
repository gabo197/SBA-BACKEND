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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

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
            builder.Entity<Appointment>().ToTable("Appointments");
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");

            //CONSTRAINTS

            //Constraints of User
            builder.Entity<User>().HasKey(user => user.Id); //Primary Key
            builder.Entity<User>().Property(user => user.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<User>().Property(user => user.Email).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(user => user.Password).IsRequired().HasMaxLength(150);
            builder.Entity<User>().Property(user => user.UserType).IsRequired().HasMaxLength(11);

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

            //Constraints of Appointment
            builder.Entity<Appointment>().HasKey(appointment => appointment.AppointmentId); //Primary Key
            builder.Entity<Appointment>().Property(appointment => appointment.Status).IsRequired().HasMaxLength(50);
            builder.Entity<Appointment>().Property(appointment => appointment.Description).IsRequired().HasMaxLength(50);
            builder.Entity<Appointment>().Property(appointment => appointment.AppointmentId).IsRequired().HasMaxLength(50);
            builder.Entity<Appointment>().Property(appointment => appointment.Valorization).IsRequired().HasMaxLength(100);

            //Constraints of PaymentMethod
            builder.Entity<PaymentMethod>().HasKey(paymentMethod => paymentMethod.PaymentMethodId); //Primary Key
            builder.Entity<PaymentMethod>().Property(paymentMethod => paymentMethod.PaymentMethodId).IsRequired().HasMaxLength(50);
            builder.Entity<PaymentMethod>().Property(paymentMethod => paymentMethod.Name).IsRequired().HasMaxLength(50);

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

            //Relationships of Appointment

            builder.Entity<Appointment>() //One to Many with Customer
                .HasOne(appointment => appointment.Customer)
                .WithMany(customer => customer.Appointments)
                .HasForeignKey(appointment => appointment.CustomerId);

            builder.Entity<Appointment>() //One to Many with Technician
                .HasOne(appointment => appointment.Technician)
                .WithMany(technician => technician.Appointments)
                .HasForeignKey(appointment => appointment.TechnicianId);

            builder.Entity<Appointment>()
                .HasOne(appointment => appointment.PaymentMethod)
                .WithMany(paymentMethod => paymentMethod.Appointments)
                .HasForeignKey(appointment => appointment.PaymentMethodId);

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
                        Password = "mu574f4",
                        UserType = "Customer"
                    },
                    new User
                    {
                        Id = 101,
                        Email = "leopoldo.murcia@gmail.com",
                        Password = "murc14",
                        UserType = "Customer"
                    },
                    new User
                    {
                        Id = 102,
                        Email = "beatriz.romero@gmail.com",
                        Password = "r0m3r0",
                        UserType = "Customer"
                    },
                    new User
                    {
                        Id = 103,
                        Email = "lucy.flores@gmail.com",
                        Password = "fl0r3s",
                        UserType = "Customer"
                    },
                    new User
                    {
                        Id = 104,
                        Email = "xabier.diaz@gmail.com",
                        Password = "d14z",
                        UserType = "Technician"
                    },
                    new User
                    {
                        Id = 105,
                        Email = "valeriano.cuellar@gmail.com",
                        Password = "cu3ll4r",
                        UserType = "Technician"
                    },
                    new User
                    {
                        Id = 106,
                        Email = "marta.tapia@gmail.com",
                        Password = "74p15",
                        UserType = "Technician"
                    },
                    new User
                    {
                        Id = 107,
                        Email = "gabriel.ramirez@gmail.com",
                        Password = "r4m1r3z",
                        UserType = "Technician"
                    },
                    new User
                    {
                        Id = 108,
                        Email = "cust",
                        Password = "cust",
                        UserType = "Customer"
                    },
                    new User
                    {
                        Id = 109,
                        Email = "tech",
                        Password = "tech",
                        UserType = "Technician"
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
                        ImageUrl = "https://randomuser.me/api/portraits/men/60.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 101,
                        FirstName = "Leopoldo",
                        LastName = "Murcia",
                        PhoneNumber = "993724957",
                        ImageUrl = "https://randomuser.me/api/portraits/men/59.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 102,
                        FirstName = "Beatriz",
                        LastName = "Romero",
                        PhoneNumber = "993724958",
                        ImageUrl = "https://randomuser.me/api/portraits/women/1.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 103,
                        FirstName = "Luciana",
                        LastName = "Flores",
                        PhoneNumber = "993724959",
                        ImageUrl = "https://randomuser.me/api/portraits/women/2.jpg",
                        Description = ""
                    },
                    new Customer
                    {
                        UserId = 108,
                        FirstName = "Jorge",
                        LastName = "Pérez",
                        PhoneNumber = "968537019",
                        ImageUrl = "https://randomuser.me/api/portraits/men/15.jpg",
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

            builder.Entity<PaymentMethod>().HasData
                (
                    new PaymentMethod
                    {
                        PaymentMethodId = 1,
                        Name = "Monedero Electrónico"
                    },
                    new PaymentMethod
                    {
                        PaymentMethodId = 2,
                        Name = "Efectivo"
                    },
                    new PaymentMethod
                    {
                        PaymentMethodId = 3,
                        Name = "Tarjeta"
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
                        Description = "acoso"
                    }
                );

            builder.Entity<Specialty>().HasData
                (
                    new Specialty
                    {
                        Id = 100,
                        Name = "Gasfitería"
                    },
                    new Specialty
                    {
                        Id = 101,
                        Name = "Electricista"
                    },
                    new Specialty
                    {
                        Id = 102,
                        Name = "Jardinería"
                    },
                    new Specialty
                    {
                        Id = 103,
                        Name = "Pintura"
                    },
                    new Specialty
                    {
                        Id = 104,
                        Name = "Limpieza"
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
                        ImageUrl = "https://randomuser.me/api/portraits/men/54.jpg",
                        Description = "Gasfitero con 10 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 105,
                        FirstName = "Valeriano",
                        LastName = "Cuellar",
                        PhoneNumber = "926503721",
                        ImageUrl = "https://randomuser.me/api/portraits/men/20.jpg",
                        Description = "Electricista con 10 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 106,
                        FirstName = "Marta",
                        LastName = "Tapia",
                        PhoneNumber = "926503728",
                        ImageUrl = "https://randomuser.me/api/portraits/women/40.jpg",
                        Description = "Jardinera con 10 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 107,
                        FirstName = "Gabriel",
                        LastName = "Ramirez",
                        PhoneNumber = "926503728",
                        ImageUrl = "https://randomuser.me/api/portraits/men/86.jpg",
                        Description = "Limpiador con 6 años de experiencia"
                    },
                    new Technician
                    {
                        UserId = 109,
                        FirstName = "Manuel",
                        LastName = "Salinas",
                        PhoneNumber = "945812940",
                        ImageUrl = "https://randomuser.me/api/portraits/men/87.jpg",
                        Description = "Pintor con 5 años de experiencia"
                    }
                );
            builder.Entity<TechnicianSpecialty>().HasData(
                    new TechnicianSpecialty
                    {
                        TechnicianId = 104,
                        SpecialtyId = 100
                    },
                    new TechnicianSpecialty
                    {
                        TechnicianId = 105,
                        SpecialtyId = 101
                    },
                    new TechnicianSpecialty
                    {
                        TechnicianId = 106,
                        SpecialtyId = 102
                    },
                    new TechnicianSpecialty
                    {
                        TechnicianId = 107,
                        SpecialtyId = 104
                    },
                    new TechnicianSpecialty
                    {
                        TechnicianId = 109,
                        SpecialtyId = 103
                    }
                );
        }
    }
}
