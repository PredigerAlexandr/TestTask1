using Microsoft.EntityFrameworkCore;

namespace TestTask1.Models.Database
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<TC> Transport_companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TransportCompany;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TC>().HasData(
                new TC[]
                {
                    //задумка в том, что Компанией Энергия выгоднее отправлять на длинные дистанции, компанией Сдек тяжёлые товары, а компанией ПЭК объёмный товары (за счёт коэффициентов)
                new TC {Id =1, Name="Энергия", CoefficientOfKilometer=1.5, CoefficientOfKilogram=7, CoefficientOfSize=7},
                new TC {Id = 2, Name="СДЕК", CoefficientOfKilometer=7, CoefficientOfKilogram=1.5, CoefficientOfSize=7},
                new TC {Id =3, Name="ПЭК", CoefficientOfKilometer=7, CoefficientOfKilogram=7, CoefficientOfSize=1.5}
                });
        }
    }
}
