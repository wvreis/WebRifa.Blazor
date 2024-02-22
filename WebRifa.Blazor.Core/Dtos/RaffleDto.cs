using System.ComponentModel.DataAnnotations;

namespace WebRifa.Blazor.Core.Dtos;
public class RaffleDto {
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    
    [Range(10, int.MaxValue, ErrorMessage = "Deve haver, no mínimo, 10 Bilhetes.")]
    public int TotalNumberOfTickets { get; set; }
    
    [Range(.01, double.MaxValue, ErrorMessage = "O Preço do Bilhete deve ser informado.")]
    public decimal TicketPrice { get; set; }
    public string Observations { get; set; } = string.Empty;

    DateTime drawDateTime;
    public DateTime DrawDateTime { 
        get => 
            drawDateTime == DateTime.MinValue ? 
            DateTime.UtcNow : 
            drawDateTime;

        set => 
            drawDateTime = value == DateTime.MinValue ? 
            drawDateTime.ToUniversalTime() : 
            value.ToUniversalTime();
    }
}