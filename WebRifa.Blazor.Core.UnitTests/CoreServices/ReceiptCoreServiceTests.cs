using Moq;
using WebRifa.Blazor.Core.CoreServices;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;

namespace WebRifa.Blazor.Core.UnitTests.CoreServices;
public class ReceiptCoreServiceTests {
    private readonly Mock<IReceiptRepository> _receiptRepositoryMock;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;

    public ReceiptCoreServiceTests()
    {
        _receiptRepositoryMock = new Mock<IReceiptRepository>();
        _ticketRepositoryMock = new Mock<ITicketRepository>();
    }

    [Fact]
    public void DeleteReceiptAsync_Success()
    {
        //Arrange
        Ticket ticket = new Ticket();
        Receipt receipt = new Receipt();
        receipt.AddTicket(ticket);

        _receiptRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(receipt);

        _receiptRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Receipt>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ticketRepositoryMock
            .Setup(repo => repo.DeleteRangeAsync(It.IsAny<List<Ticket>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        ReceiptCoreService service = BuildReceiptCoreService();

        //Act
        var result = service.DeleteReceiptAsync(new(), CancellationToken.None);

        //Assert
        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task DeleteReceiptAsync_WhenReceiptNotFound_ThrowsException()
    {
        //Arrange
        Receipt? receipt = null;

        _receiptRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
            .ReturnsAsync(receipt);

        var service = BuildReceiptCoreService();

        //Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            service.DeleteReceiptAsync(new(), CancellationToken.None));
    }
    private ReceiptCoreService BuildReceiptCoreService()
    {
        return new ReceiptCoreService(
            _receiptRepositoryMock.Object,
            _ticketRepositoryMock.Object);
    }
}
