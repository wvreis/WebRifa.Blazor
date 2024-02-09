
namespace WebRifa.Blazor.Core.Entities;
public class Buyer : BaseEntity {
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    public List<BuyerTicketReceipt>? BuyerTicketReceipts { get; private set; }

    public Buyer()
    {
            
    }

    public void Update(
        string? name,
        string? phoneNumber,
        string? email)
    {
        if (!string.IsNullOrEmpty(name)) {
            Name = name!;
        }

        if (!string.IsNullOrEmpty(phoneNumber)) {
            PhoneNumber = phoneNumber!;
        }

        if (!string.IsNullOrEmpty(email)) {
            Email = email!;
        }
    }
}