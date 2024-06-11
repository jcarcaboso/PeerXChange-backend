using Mapster;
using Domain = UsersManagement.Domain;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.User, User>()
            .Map(dst => dst.Wallet, src => src.Wallet)
            .Map(dst => dst.Language, src => src.Language.ToString().ToLowerInvariant());
    }
}