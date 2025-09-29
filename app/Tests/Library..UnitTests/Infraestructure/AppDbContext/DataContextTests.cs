using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.UnitTests.Infraestructure.AppDbContext;

public class DataContextTests
{
    private DataContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new DataContext(options);
    }

    [Fact]
    public void ShouldCreateDbSets()
    {
        // Arrange
        var context = CreateInMemoryContext();

        // Act & Assert
        context.Books.Should().NotBeNull();
        context.Users.Should().NotBeNull();
        context.AuditLog.Should().NotBeNull();
    }

    [Fact]
    public void OnModelCreating_ShouldApplyConfigurations()
    {
        // Arrange
        var context = CreateInMemoryContext();

        // Act
        var model = context.Model;

        // Assert
        model.GetEntityTypes().Any(e => e.ClrType == typeof(Book)).Should().BeTrue();
        model.GetEntityTypes().Any(e => e.ClrType == typeof(Login)).Should().BeTrue();
        model.GetEntityTypes().Any(e => e.ClrType == typeof(AuditLog)).Should().BeTrue();
    }
}