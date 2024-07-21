namespace EscrowManagement.Persistence;

public interface IOfferRepository
{
    Task<object> GetAsync(string id);
    Task UpsertAsync(string id);
    Task DeleteAsync(string id);
}
