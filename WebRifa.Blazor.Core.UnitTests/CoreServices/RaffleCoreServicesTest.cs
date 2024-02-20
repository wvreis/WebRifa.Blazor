using Moq;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Core.UnitTests.CoreServices;

public class RaffleCoreServicesTest
{
    private readonly Mock<IReceiptRepository> _receiptRepositoryMock;
    private readonly Mock<IBuyerRepository> _buyerRepositoryMock;
    private readonly Mock<IRaffleRepository> _raffleRepositoryMock;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IDrawRepository> _drawRepositoryMock;
    private readonly Mock<IBuyerTicketReceiptRepository> _buyerTicketReceiptRepositoryMock;

    public RaffleCoreServicesTest()
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
        var usedNumbers = new HashSet<int> { 4, 5, 6 };
        int totalOfNumbers = 6;
        Buyer buyer = new();

        _buyerRepositoryMock
            .Setup(repo => repo.GetAsync(command.BuyerId.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(buyer);

        _receiptRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _raffleRepositoryMock
            .Setup(repo => repo.GetUsedNumbersAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usedNumbers); 
        
        _raffleRepositoryMock
            .Setup(repo => repo.GetTotalNumberOfTicketsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOfNumbers);

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
        _receiptRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()), Times.Once);
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

        var freeNumbers = new HashSet<int> { 1, 2, 3 };
        var usedNumbers = new HashSet<int> { 1, 2, 4 };
        int totalOfNumbers = 6;
        Buyer buyer = new();

        _buyerRepositoryMock
            .Setup(repo => repo.GetAsync(command.BuyerId.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(buyer);

        _receiptRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _raffleRepositoryMock
            .Setup(repo => repo.GetUsedNumbersAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usedNumbers);

        _raffleRepositoryMock
            .Setup(repo => repo.GetTotalNumberOfTicketsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOfNumbers);

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

    [Fact]
    public async Task CarryOutTheDrawAsync_Success()
    {
        // Arrange
        var raffleId = new Guid();
        var usedNumbers = new HashSet<int> { 1, 2, 4 };
        int totalOfNumbers = 6;

        var ticket1 = new Ticket(1, string.Empty, new Guid());
        var ticket2 = new Ticket(2, string.Empty, new Guid());
        var ticket4 = new Ticket(4, string.Empty, new Guid());

        var tickets = new List<Ticket> { ticket1, ticket2, ticket4 };

        var raffle = new Raffle(
            "Teste",
            totalOfNumbers,
            10M,
            string.Empty,
            DateTime.UtcNow,
            tickets);

        _raffleRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(raffle);

        _raffleRepositoryMock
            .Setup(repo => repo.GetUsedNumbersAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usedNumbers);

        _raffleRepositoryMock
            .Setup(repo => repo.GetTotalNumberOfTicketsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOfNumbers);

        _drawRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Draw>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new RaffleCoreService(
            _receiptRepositoryMock.Object,
            _buyerRepositoryMock.Object,
            _raffleRepositoryMock.Object,
            _ticketRepositoryMock.Object,
            _drawRepositoryMock.Object,
            _buyerTicketReceiptRepositoryMock.Object);

        // Act
        await service.CarryOutTheDrawAsync(raffleId, CancellationToken.None);

        // Assert
        _drawRepositoryMock.VerifyAll();
    }
}