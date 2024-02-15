namespace WebRifa.Blazor.Core.Dtos;

public class ReceiptDto {
    public Guid Id { get; set; }
    public List<TicketDto>? Tickets { get; set; }
}