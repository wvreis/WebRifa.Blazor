namespace WebRifa.Blazor.Core.Dtos;
public class BuyerTicketReceiptDto {
    public Guid BuyerId { get; set; }
    public BuyerDto Buyer { get; set; } = new();

    public Guid TicketId { get; set; }
    public TicketDto Ticket { get; set; } = new();

    public Guid ReceiptId { get; set; }
    //public ReceiptDto Receipt { get; set; } = new();
}