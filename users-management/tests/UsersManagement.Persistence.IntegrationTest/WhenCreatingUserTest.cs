using System.Net;
using FluentAssertions;

namespace UsersManagement.Persistence.IntegrationTest;

public sealed class WhenCreatingUserTest : BaseIntegrationTest
{
    private const string UserController = "api/user";
    
    public WhenCreatingUserTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task When_user_not_exist_should_return_created()
    {
        const string wallet = "0x000001";
        const string language = "EN";
        var msg = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{UserController}?wallet={wallet}&language={language}", UriKind.Relative),
        };

        var response = await HttpClient.SendAsync(msg);
        
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var user = await Context.Users.FindAsync([wallet]);
        user.Should().NotBeNull();
        user!.Language.Should().Be(language);
    }
    
    [Fact]
    public async Task When_getting_user_should_return_user()
    {
        const string wallet = "0x000001";
        var msg = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{UserController}?wallet={wallet}", UriKind.Relative),
        };

        var response = await HttpClient.SendAsync(msg);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // response.
    }
}