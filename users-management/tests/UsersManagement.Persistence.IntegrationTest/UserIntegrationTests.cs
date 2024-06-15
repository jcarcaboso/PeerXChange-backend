using System.Net;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence.IntegrationTest;

public sealed class UserIntegrationTests : BaseIntegrationTest
{
    private const string UserController = "api/user";
    
    public UserIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task When_creating_an_user_should_return_created()
    {
        const string wallet = "0x000001";
        const string language = "en";
        var msg = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{UserController}?wallet={wallet}&language={language}", UriKind.Relative),
        };

        var response = await HttpClient.SendAsync(msg);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var user = await Context.Users.FindAsync([wallet]);
        user.Should().NotBeNull();
        user!.Language.Should().Be(language);
    }
}