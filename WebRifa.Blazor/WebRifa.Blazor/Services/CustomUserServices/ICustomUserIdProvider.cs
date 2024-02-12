namespace WebRifa.Blazor.Services.UserServices;

public interface ICustomUserIdProvider
{
    Task<Guid> GetUserIdAsync();
}
