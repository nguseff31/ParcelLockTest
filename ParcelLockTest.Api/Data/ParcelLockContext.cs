using Microsoft.EntityFrameworkCore;
using ParcelLockTest.Api.Entities;

#pragma warning disable CS8618

namespace ParcelLockTest.Api.Data;

public class ParcelLockContext : DbContext {
    public DbSet<ParcelLockDto> ParcelLocks { get; set; }

    public DbSet<OrderDto> Orders { get; set; }

    public ParcelLockContext(DbContextOptions<ParcelLockContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ParcelLockDto>()
            .HasData(new ParcelLockDto {
                Number = "1111-111",
                Address = "Москва, улица Маршала Новикова, 14 к2",
                IsActive = true
            }, new ParcelLockDto {
                Number = "2222-222",
                Address = "Москва, Маршала Бирюзова, 32",
                IsActive = true
            });
    }
}
