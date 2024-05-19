using Animal_Shelter.Entities;
using Microsoft.EntityFrameworkCore;
namespace Animal_Shelter.Data;

public class AnimalShelterDbContext : DbContext
{
    public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options) : base (options) { }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Animals> Animals { get; set; }
    public DbSet<Costs> Costs { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<AnimalSize> AnimalSizes { get; set; }
    public DbSet<PaymentPeriod> PaymentPeriods { get; set; }
    public DbSet<CostsCategory> CostsCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shelter>().HasData(
        new Shelter{ShelterId = 1, Email = "happyanimals@email.com", Name = "Happy Animals", HashedPassword = "happyanimals"},
        new Shelter{ShelterId = 2, Email = "pipsqueakery@email.com", Name = "Pipsqueakery: A Shelter For Small Animals", HashedPassword = "pipsqueakery"}
        );
        modelBuilder.Entity<AnimalType>().HasData(
            new AnimalType() { AnimalTypeId = (int)AnimalTypeEnum.Dog, Value = "Dog" },
            new AnimalType() { AnimalTypeId = (int)AnimalTypeEnum.Cat, Value = "Cat" },
            new AnimalType() { AnimalTypeId = (int)AnimalTypeEnum.Other, Value = "Other" }
        );
        modelBuilder.Entity<AnimalSize>().HasData(
            new AnimalSize() { AnimalSizeId = (int)AnimalSizeEnum.Small , Value = "Small" },
            new AnimalSize() { AnimalSizeId = (int)AnimalSizeEnum.Medium, Value = "Medium" },
            new AnimalSize() { AnimalSizeId = (int)AnimalSizeEnum.Large, Value = "Large" }
        );
        modelBuilder.Entity<PaymentPeriod>().HasData(
            new PaymentPeriod() { PaymentPeriodId = (int)PaymentPeriodEnum.Daily, Value = "Daily" },
            new PaymentPeriod() { PaymentPeriodId = (int)PaymentPeriodEnum.Monthly, Value = "Monthly" },
            new PaymentPeriod() { PaymentPeriodId = (int)PaymentPeriodEnum.Yearly, Value = "Yearly" }
        );
        modelBuilder.Entity<CostsCategory>().HasData(
            new CostsCategory() { CostCategoryId = (int)CostsCategoryEnum.Food, Value = "Food" },
            new CostsCategory() { CostCategoryId = (int)CostsCategoryEnum.Employees, Value = "Employees" },
            new CostsCategory() { CostCategoryId = (int)CostsCategoryEnum.Maintenance, Value = "Maintenance" }
        );
        modelBuilder.Entity<Animals>().HasData(
            // SPACJA JAK BEDZIE NIE GRZECZNA
            new Animals() { AnimalId = 1, AnimalName = "Spacja", ShelterId = 1, SizeId = (int)AnimalSizeEnum.Medium, TypeId = (int)AnimalTypeEnum.Dog});
        modelBuilder.Entity<Costs>().HasData(
            new Costs() {CostId = 1, ShelterId = 1, CostName = "Pensja" , Cost = 5000, CategoryId = (int)CostsCategoryEnum.Employees, 
                PaymentPeriodId = (int)PaymentPeriodEnum.Monthly, CostDescription = "Wypłata dla Jana Kowalskiego"}
        );
    }
}