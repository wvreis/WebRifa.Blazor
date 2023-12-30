using WebRifa.Blazor.Core.Common;

namespace WebRifa.Blazor.Core.Entities;
public class Buyer : BaseEntity {
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
}