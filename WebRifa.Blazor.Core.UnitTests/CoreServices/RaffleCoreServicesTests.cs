using Moq;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Core.UnitTests.CoreServices;

public class RaffleCoreServicesTests
{
    private readonly Mock<IReceiptRepository> _receiptRepositoryMock;
    private readonly Mock<IBuyerRepository> _buyerRepositoryMock;
    private readonly Mock<IRaffleRepository> _raffleRepositoryMock;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IDrawRepository> _drawRepositoryMock;
    private readonly Mock<IBuyerTicketReceiptRepository> _buyerTicketReceiptRepositoryMock;

    public RaffleCoreServicesTests()
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
        var command = GetBuyRaffleTicketsCommand();

        var freeNumbers = new HashSet<int> { 1, 2, 3 };
        var usedNumbers = new HashSet<int> { 4, 5, 6 };
        int totalOfNumbers = 6;
        Buyer buyer = new();

        _buyerRepositoryMock
            .Setup(repo => repo.GetAsync(command.BuyerId!.Value, It.IsAny<CancellationToken>()))
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

        RaffleCoreService service = BuildCoreService();

        // Act
        await service.BuyRaffleTicketsAsync(command, CancellationToken.None);

        // Assert
        _receiptRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task BuyRaffleTicketsAsync_NotAllNumbersAvailable_ThrowsException()
    {
        //Arrange
        BuyRaffleTicketsCommand command = GetBuyRaffleTicketsCommand();

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

        RaffleCoreService service = BuildCoreService();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.BuyRaffleTicketsAsync(command, CancellationToken.None));
    }

    [Fact]
    public async Task CarryOutTheDrawAsync_DrawNumberIsInUsedNumbers_Success()
    {
        // Arrange
        var raffleId = new Guid();
        var usedNumbers = new HashSet<int> { 1, 2, 4 };
        int totalOfNumbers = 6;

        var ticket1 = GetTicket(1);
        var ticket2 = GetTicket(2);
        var ticket4 = GetTicket(4);

        var tickets = new List<Ticket> { ticket1, ticket2, ticket4 };

        var raffle = GetRaffle(totalOfNumbers, tickets);

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

        RaffleCoreService service = BuildCoreService();

        // Act
        var result = await service.CarryOutTheDrawAsync(raffleId, CancellationToken.None);

        // Assert
        Assert.Contains(result, usedNumbers);
        _drawRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task CarryOutTheDrawAsync_IsRaffleNull_ThrowsException()
    {
        //Arrange
        Raffle? raffle = null;
        var usedNumbers = new HashSet<int> { 0 };
        var totalOfNumbers = 0;

        _raffleRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(raffle);

        _raffleRepositoryMock
            .Setup(repo => repo.GetUsedNumbersAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usedNumbers);

        _raffleRepositoryMock
            .Setup(repo => repo.GetTotalNumberOfTicketsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOfNumbers);

        RaffleCoreService service = BuildCoreService();

        //Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
             service.CarryOutTheDrawAsync(new(), CancellationToken.None));
    }

    [Theory]
    [InlineData(5, new int[] { 1, 2, 3 })]
    [InlineData(10, new int[] { 1, 2, 3, 5, 9 })]
    [InlineData(100, new int[] { 1, 2, 3, 8, 76 })]
    public async Task GetFreeNumbersAsync_FreeNumbersDoNotConflictWithUsedNumbers(int totalOfNumbers, int[] usedNumbers)
    {
        //Arrange
        HashSet<int> usedNumbersHashset = usedNumbers.ToHashSet();

        _raffleRepositoryMock
            .Setup(repo => repo.GetTotalNumberOfTicketsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOfNumbers);

        _raffleRepositoryMock
            .Setup(repo => repo.GetUsedNumbersAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(usedNumbersHashset);

        RaffleCoreService service = BuildCoreService();

        //Act
        var freeNumbers = await service.GetFreeNumbersAsync(new(), CancellationToken.None);

        //Assert
        Assert.DoesNotContain(freeNumbers.ToList(), fn => usedNumbers.Contains(fn));
    }

    [Fact]
    public void DeleteRaffleAsync_Success()
    {
        //Arrange
        var ticket1 = GetTicket(1);
        
        List<Ticket> tickets = new() { ticket1 };
        
        Raffle raffle = GetRaffle(1, tickets);
        
        BuyerTicketReceipt buyerTicketReceipt = GetBuyerTicketReceipt(new(), new(), new());
        
        List<BuyerTicketReceipt> buyerTicketReceipts = new() { buyerTicketReceipt };
        
        Receipt receipt = GetReceipt(buyerTicketReceipts);
        
        List<Receipt> receiptList = new() { receipt };

        _raffleRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(raffle);

        _receiptRepositoryMock
            .Setup(repo => repo.GetReceiptsFromRaffleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiptList);

        _ticketRepositoryMock
            .Setup(repo => repo.DeleteRangeAsync(It.IsAny<List<Ticket>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _buyerTicketReceiptRepositoryMock
            .Setup(repo => repo.DeleteRangeAsync(It.IsAny<List<BuyerTicketReceipt>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _raffleRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Raffle>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        RaffleCoreService service = BuildCoreService();

        //Act
        var result = service.DeleteRaffleAsync(new(), CancellationToken.None);

        //Assert
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task DeleteRaffleAsync_RaffleNull_ThrowsException()
    {
        Raffle? raffle = null;

        //Arrange
        _raffleRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(raffle);

        RaffleCoreService service = BuildCoreService();

        //Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => 
            service.DeleteRaffleAsync(new(), CancellationToken.None));
    }

    private RaffleCoreService BuildCoreService()
    {
        return new RaffleCoreService(
            _receiptRepositoryMock.Object,
            _buyerRepositoryMock.Object,
            _raffleRepositoryMock.Object,
            _ticketRepositoryMock.Object,
            _drawRepositoryMock.Object,
            _buyerTicketReceiptRepositoryMock.Object);
    }

    private Raffle GetRaffle(
        int totalOfNumbers,
        List<Ticket>? tickets = null)
    {
        return new Raffle(
            "Teste",
            totalOfNumbers,
            1M,
            string.Empty,
            DateTime.UtcNow,
            tickets);
    }

    private Ticket GetTicket(
        int number,
        List<BuyerTicketReceipt>? buyerTicketReceipts = null)
    {
        return new Ticket(
            number,
            string.Empty,
            new Guid(),
            buyerTicketReceipts);
    }

    private Receipt GetReceipt(List<BuyerTicketReceipt>? buyerTicketReceipts = null)
    {
        return new Receipt(buyerTicketReceipts);
    }

    private BuyerTicketReceipt GetBuyerTicketReceipt(
        Guid buyerId,
        Guid ticketId,
        Guid receiptId)
    {
        return new BuyerTicketReceipt(
            buyerId, 
            ticketId, 
            receiptId);
    }

    private BuyRaffleTicketsCommand GetBuyRaffleTicketsCommand()
    {
        return new BuyRaffleTicketsCommand {
            RaffleId = new(),
            BuyerId = new(),
            NumbersToBuy = new HashSet<int> { 1, 2, 3 },
            Observations = "Test"
        };
    }
}