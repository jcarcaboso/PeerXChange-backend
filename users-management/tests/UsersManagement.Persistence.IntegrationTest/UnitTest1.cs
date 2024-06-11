using Microsoft.EntityFrameworkCore;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence.IntegrationTest;

public sealed class UnitTest1 : BaseIntegrationTest
{
    public UnitTest1(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Test1()
    {
        // await Context.Wallets.AddAsync(new Wallet()
        // {
        //     Address = "0x000001",
        //     Language = "en"
        // });
        // await Context.SaveChangesAsync();
        //
        // var wallets = Context.Wallets.AsNoTracking().ToList();
        //
        // var a = wallets;
    }

}