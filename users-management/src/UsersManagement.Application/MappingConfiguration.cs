using Mapster;
using UsersManagement.Application.CreateUser;
using UsersManagement.Application.GetUser;
using UsersManagement.Application.UpdateUser;
using UsersManagement.Domain;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Application;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserCommand, Wallet>()
            .Map(dst => dst.Address, src => src.Wallet)
            .Map(dst => dst.Language, src => src.Language.ToString().ToLowerInvariant());

        config.NewConfig<User, GetUserQueryResponse>()
            .Map(dst => dst.Address, src => src.Wallet)
            .Map(dst => dst.Language, src => src.Language)
            .Map(dst => dst.Role, src => src.Role.ToString());
        
        config.NewConfig<UpdateUserCommand, PartialUser>()
            .Map(dst => dst.Language, src => src.Language);
    }
}