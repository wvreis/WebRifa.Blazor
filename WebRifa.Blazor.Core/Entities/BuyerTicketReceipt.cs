namespace WebRifa.Blazor.Core.Entities;
public class BuyerTicketReceipt : BaseEntity
{
    public Guid BuyerId { get; private set; }
    public Buyer? Buyer { get; private set; }

    public Guid TicketId { get; private set; }
    public Ticket? Ticket { get; private set; }

    public Guid ReceiptId { get; private set; }
    public Receipt? Receipt { get; private set; }

    public BuyerTicketReceipt()
    {
         
    }

    public BuyerTicketReceipt(
        Buyer buyer,
        Ticket ticket,
        Receipt receipt)
    {
        BuyerId = buyer.Id;
        TicketId = ticket.Id;
        ReceiptId = receipt.Id;
        Buyer = buyer;
        Ticket = ticket;
        Receipt = receipt;
    }

    public BuyerTicketReceipt(
        Guid buyerId,
        Guid ticketId,
        Guid receiptId)
    {
        BuyerId = buyerId;
        TicketId = ticketId;
        ReceiptId = receiptId;
    }
}