using System.ComponentModel.DataAnnotations;
using WebRifa.Blazor.Core.Attributes;

namespace WebRifa.Blazor.Core.Requests.Commands;
public class BuyRaffleTicketsCommand
{
    [Required(ErrorMessage = "Uma Rifa deve ser selecionada.")]
    public Guid? RaffleId { get; set; }

    [Required(ErrorMessage = "Um Comprador deve ser selecionado")]
    public Guid? BuyerId { get; set; }

    [HashSetHasElements(ErrorMessage = "Pelo menos um número deve ser selecionado.")]
    public HashSet<int> NumbersToBuy { get; set; } = new();
    public string Observations { get; set; } = string.Empty;
}