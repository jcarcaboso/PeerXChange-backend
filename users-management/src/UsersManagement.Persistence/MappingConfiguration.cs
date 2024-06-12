using Mapster;
using Domain = UsersManagement.Domain;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<User, Domain.UserWithoutRole>()
        //     .Map(dst => dst.Wallet, src => src.Wallet);
    }
}