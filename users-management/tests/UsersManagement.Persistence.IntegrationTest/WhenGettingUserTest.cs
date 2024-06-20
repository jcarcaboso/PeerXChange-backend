using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using PeerXChange.Common;
using UsersManagement.Api.Contracts;
using UsersManagement.Application.GetUser;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence.IntegrationTest;

public sealed class WhenGettingUserTest : BaseIntegrationTest
{
    private const string UserController = "api/user";
    
    public WhenGettingUserTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task When_getting_user_should_return_user()
    {
        const string wallet = "0x000001";

        await Context.Users.AddAsync(new User()
        {
            Wallet = wallet,
            Language = "en",
            Role = (int)Domain.Role.User,
        });
        await Context.SaveChangesAsync();
        var expected = new GetUserResponse()
        {
            Address = wallet,
            Language = Language.EN,
            Role = Domain.Role.User
        };
        var msg = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{UserController}?wallet={wallet}", UriKind.Relative),
        };

        var response = await HttpClient.SendAsync(msg);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var rs = await response.Content.ReadAsStringAsync();
        var opt = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        var user = JsonSerializer.Deserialize<GetUserResponse>(rs, opt);
        user.Should().BeEquivalentTo(expected);
    }
}