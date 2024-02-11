using System.ComponentModel.DataAnnotations;
using WebRifa.Blazor.Core.Attributes;

namespace WebRifa.Blazor.Core.Dtos;

public class BuyerDto {
    [Required]
    public Guid Id { get; set; }

    [MinLength(3, ErrorMessage = "O nome deve conter, no mínimo, 3 letras.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Insira um telefone válido.")]
    [RegularExpression(@"^\+?(\d{2})?\s?(\()?\d{2}(\))?[\s\.-]?\d{4,5}[\s\.-]?\d{4}$", ErrorMessage = "Insira um telefone válido.")]

    public string PhoneNumber { get; set; } = string.Empty;

    [CustomEmailAddress(ErrorMessage = "Insira um E-mail válido")]
    public string Email { get; set; } = string.Empty;
}