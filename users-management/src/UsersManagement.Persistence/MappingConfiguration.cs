using Mapster;
using UsersManagement.Domain;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, Wallet>()
            .Map(dst => dst.Address, src => src.Wallet)
            .Map(dst => dst.Language, src => src.Language.ToString().ToLowerInvariant());
    }
}