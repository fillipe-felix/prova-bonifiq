using Moq;

using ProvaPub.Models;
using ProvaPub.Providers.Interfaces;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;

using Xunit;

namespace ProvaPub.Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _customerService = new CustomerService(_customerRepositoryMock.Object, _dateTimeProviderMock.Object);
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowException_WhenCustomerIdIsInvalid()
    {
        // Arrange Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(0, 50));
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowException_WhenPurchaseValueIsInvalid()
    {
        // Arrange Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(1, 0));
    }

    [Fact]
    public async Task CanPurchase_ShouldThrowException_WhenCustomerDoesNotExist()
    {
        // Arrange
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer?)null);
        
        // Act Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _customerService.CanPurchase(1, 50));
    }

    [Fact]
    public async Task CanPurchase_ShouldReturnFalse_WhenCustomerHasPurchasedInLastMonth()
    {
        // Arrange
        var customerId = 1;
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        var currentDate = new DateTime(2024, 1, 10, 10, 0, 0, DateTimeKind.Utc);
        _dateTimeProviderMock.Setup(dtp => dtp.UtcNow).Returns(currentDate);

        var baseDate = currentDate.AddMonths(-1);

        _customerRepositoryMock.Setup(repo => repo.HasPurchasedInLastMonthAsync(customerId, baseDate))
            .ReturnsAsync(true);

        // Act
        var result = await _customerService.CanPurchase(customerId, 50);

        // Assert
        Assert.False(result);
    }


    [Fact]
    public async Task CanPurchase_ShouldReturnFalse_WhenNewCustomerExceedsFirstPurchaseLimit()
    {
        // Arrange
        var customerId = 1;
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId))
            .ReturnsAsync(new Customer());

        var currentDate = new DateTime(2024, 1, 10, 10, 0, 0, DateTimeKind.Utc);
        _dateTimeProviderMock.Setup(dtp => dtp.UtcNow).Returns(currentDate);

        _customerRepositoryMock.Setup(repo => repo.HasPurchasedInLastMonthAsync(customerId, It.IsAny<DateTime>()))
            .ReturnsAsync(false);

        _customerRepositoryMock.Setup(repo => repo.HasEverPurchasedAsync(customerId))
            .ReturnsAsync(false);

        // Act
        var result = await _customerService.CanPurchase(customerId, 150);

        // Assert
        Assert.False(result);
    }


    [Fact]
    public async Task CanPurchase_ShouldReturnFalse_WhenOutsideBusinessHours()
    {
        // Arrange
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
        _customerRepositoryMock.Setup(repo => repo.HasPurchasedInLastMonthAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(false);
        _customerRepositoryMock.Setup(repo => repo.HasEverPurchasedAsync(It.IsAny<int>())).ReturnsAsync(true);
        _dateTimeProviderMock.Setup(dtp => dtp.UtcNow).Returns(new DateTime(2024, 1, 1, 7, 0, 0, DateTimeKind.Utc));

        // Act
        var result = await _customerService.CanPurchase(1, 50);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CanPurchase_ShouldReturnTrue_WhenAllConditionsAreMet()
    {
        // Arrange
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
        _customerRepositoryMock.Setup(repo => repo.HasPurchasedInLastMonthAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(false);
        _customerRepositoryMock.Setup(repo => repo.HasEverPurchasedAsync(It.IsAny<int>())).ReturnsAsync(true);
        _dateTimeProviderMock.Setup(dtp => dtp.UtcNow).Returns(new DateTime(2024, 1, 2, 10, 0, 0, DateTimeKind.Utc));

        // Act
        var result = await _customerService.CanPurchase(1, 50);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ListCustomers_ShouldReturnPagedList()
    {
        // Arrange
        var customers = new List<Customer?>
        {
            new Customer { Id = 1, Name = "John" },
            new Customer { Id = 2, Name = "Jane" },
            new Customer { Id = 3, Name = "Jack" },
        };
        _customerRepositoryMock
            .Setup(repo => repo.GetAll())
            .Returns(customers.AsQueryable());

        // Act
        var result = _customerService.ListCustomers(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(customers, result.Customers);
    }
}