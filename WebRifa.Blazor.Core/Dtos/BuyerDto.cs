using System.ComponentModel.DataAnnotations;

namespace WebRifa.Blazor.Core.Dtos;

public class BuyerDto {
    [Required]
    public Guid Id { get; set; }

    [MinLength(3, ErrorMessage = "O nome deve conter, no mínimo, 3 letras.")]
    public string Name { get; set; } = string.Empty;

    [RegularExpression("^\\([1-9]{2}\\) (?:[2-8]|9[0-9])[0-9]{3}\\-[0-9]{4}$", ErrorMessage = "Insira um telefone válido.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Insira um e-mail válido.")]
    public string Email { get; set; } = string.Empty;
}