using Moq;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Core.UnitTests.CoreServices;

public class RaffleCoreServices
{
    private readonly Mock<IReceiptRepository> _receiptRepositoryMock;
    private readonly Mock<IBuyerRepository> _buyerRepositoryMock;
    private readonly Mock<IRaffleRepository> _raffleRepositoryMock;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IDrawRepository> _drawRepositoryMock;
    private readonly Mock<IBuyerTicketReceiptRepository> _buyerTicketReceiptRepositoryMock;

    public RaffleCoreServices()
    {
        _receiptRepositoryMock = new Mock<IReceiptRepository>();
        _buyerRepositoryMock = new Mock<IBuyerRepository>();
        _raffleRepositoryMock = new Mock<IRaffleRepository>();
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _drawRepositoryMock = new Mock<IDrawRepository>();
        _buyerTicketReceiptRepositoryMock = new Mock<IBuyerTicketReceiptRepository>();
    }

    [Fact]
    public async Task BuyRaffleTicketsAsync_AllNumbersAvailable_Success()
    {
        // Arrange
        var command = new BuyRaffleTicketsCommand {
            RaffleId = new(),
            BuyerId = new(),
            NumbersToBuy = new HashSet<int> { 1, 2, 3 },
            Observations = "Test"
        };

        var freeNumbers = new HashSet<int> { 1, 2, 3 };
        Buyer buyer = new();

        var buyerRepositoryMock = new Mock<IBuyerRepository>();
        buyerRepositoryMock.Setup(repo => repo.GetAsync(command.BuyerId.Value, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(buyer);

        var receiptRepositoryMock = new Mock<IReceiptRepository>();
        receiptRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()))
                                .Returns(Task.CompletedTask);



        var service = new RaffleCoreService(
            _receiptRepositoryMock.Object,
            _buyerRepositoryMock.Object,
            _raffleRepositoryMock.Object,
            _ticketRepositoryMock.Object,
            _drawRepositoryMock.Object,
            _buyerTicketReceiptRepositoryMock.Object);

        // Act
        await service.BuyRaffleTicketsAsync(command, CancellationToken.None);

        // Assert
        receiptRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task BuyRaffleTicketsAsync_NotAllNumbersAvailable_ThrowsException()
    {
        // Arrange
        var command = new BuyRaffleTicketsCommand {
            RaffleId = new(),
            BuyerId = new(),
            NumbersToBuy = new HashSet<int> { 1, 2, 3 },
            Observations = "Test"
        };

        var freeNumbers = new List<int> { 1, 3 }; // Number 2 is not available
        Buyer buyer = new();

        var buyerRepositoryMock = new Mock<IBuyerRepository>();
        buyerRepositoryMock
            .Setup(repo => repo.GetAsync(command.BuyerId.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(buyer);

        var service = new RaffleCoreService(
            _receiptRepositoryMock.Object,
            _buyerRepositoryMock.Object,
            _raffleRepositoryMock.Object,
            _ticketRepositoryMock.Object,
            _drawRepositoryMock.Object,
            _buyerTicketReceiptRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            service.BuyRaffleTicketsAsync(command, CancellationToken.None));
    }
    
}