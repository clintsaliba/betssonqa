using Betsson.OnlineWallets.Data.Models;
using Betsson.OnlineWallets.Data.Repositories;
using Betsson.OnlineWallets.Exceptions;
using Betsson.OnlineWallets.Services;
using Betsson.OnlineWallets.Tests.Services.Builders;
using Moq;
using Xunit;

namespace Betsson.OnlineWallets.Tests.Services;

public class OnlineWalletServiceTests
{
    private readonly Mock<IOnlineWalletRepository> _repositoryMock;
    private readonly OnlineWalletService _service;

    public OnlineWalletServiceTests()
    {
        _repositoryMock = new Mock<IOnlineWalletRepository>();
        _service = new OnlineWalletService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetBalanceAsync_NoTransactions_ReturnsZeroBalance()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetLastOnlineWalletEntryAsync())
            .ReturnsAsync((OnlineWalletEntry)null);

        // Act
        var result = await _service.GetBalanceAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.Amount);
    }

    [Fact]
    public async Task DepositFundsAsync_ValidAmount_IncreasesBalance()
    {
        // Arrange
        var initialBalance = 100m;
        var depositAmount = 50m;

        _repositoryMock.Setup(r => r.GetLastOnlineWalletEntryAsync())
            .ReturnsAsync(new OnlineWalletServiceBuilder()
                .WithBalanceBefore(100)
                .WithAmount(0)
                .Build());

        _repositoryMock.Setup(r => r.InsertOnlineWalletEntryAsync(It.IsAny<OnlineWalletEntry>()))
            .Returns(Task.CompletedTask);

        var deposit = new DepositBuilder()
            .WithAmount(depositAmount)
            .Build();

        // Act
        var result = await _service.DepositFundsAsync(deposit);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(150, result.Amount);
        _repositoryMock.Verify(r => r.InsertOnlineWalletEntryAsync(It.Is<OnlineWalletEntry>(
            entry => entry.Amount == depositAmount && entry.BalanceBefore == initialBalance)), Times.Once);
    }

    [Fact]
    public async Task WithdrawFundsAsync_ValidAmount_DecreasesBalance()
    {
        // Arrange
        var initialBalance = 200m;
        var withdrawalAmount = 50m;

        _repositoryMock.Setup(r => r.GetLastOnlineWalletEntryAsync())
            .ReturnsAsync(new OnlineWalletServiceBuilder()
                .WithBalanceBefore(200)
                .WithAmount(0)
                .Build());

        _repositoryMock.Setup(r => r.InsertOnlineWalletEntryAsync(It.IsAny<OnlineWalletEntry>()))
            .Returns(Task.CompletedTask);

        var withdrawal = new WithdrawalBuilder()
            .WithAmount(withdrawalAmount)
            .Build();

        // Act
        var result = await _service.WithdrawFundsAsync(withdrawal);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(150, result.Amount);
        _repositoryMock.Verify(r => r.InsertOnlineWalletEntryAsync(It.Is<OnlineWalletEntry>(
            entry => entry.Amount == -withdrawalAmount && entry.BalanceBefore == initialBalance)), Times.Once);
    }

    [Fact]
    public async Task WithdrawFundsAsync_InsufficientBalance_ThrowsException()
    {
        // Arrange
        var initialBalance = 50m;
        var withdrawalAmount = 100m;

        _repositoryMock.Setup(r => r.GetLastOnlineWalletEntryAsync())
            .ReturnsAsync(new OnlineWalletServiceBuilder()
                .WithBalanceBefore(initialBalance)
                .WithAmount(0)
                .Build());

        var withdrawal = new WithdrawalBuilder()
            .WithAmount(withdrawalAmount)
            .Build();

        // Act & Assert
        await Assert.ThrowsAsync<InsufficientBalanceException>(() => _service.WithdrawFundsAsync(withdrawal));
    }
}