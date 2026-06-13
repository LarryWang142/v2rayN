using AwesomeAssertions;
using ServiceLib.Enums;
using ServiceLib.Handler;
using ServiceLib.Models;
using ServiceLib.Tests.CoreConfig;
using Xunit;

namespace ServiceLib.Tests.Handler;

public class ConfigHandlerTests
{
    [Theory]
    [InlineData(false, "h2")]
    [InlineData(true, "h3")]
    public async Task AddTrustTunnelServer_ShouldApplyExpectedDefaults(bool quicEnabled, string expectedAlpn)
    {
        var config = CoreConfigTestFactory.CreateConfig(ECoreType.sing_box);
        var profile = new ProfileItem
        {
            Address = "  land.gomacondo.com  ",
            Port = 443,
            Username = "  tt_inbound  ",
            Password = "  secret-pass  ",
            StreamSecurity = string.Empty,
            Alpn = string.Empty,
        };
        profile.SetProtocolExtra(profile.GetProtocolExtra() with
        {
            NaiveQuic = quicEnabled ? true : null,
        });

        var result = await ConfigHandler.AddTrustTunnelServer(config, profile, false);

        result.Should().Be(0);
        profile.ConfigType.Should().Be(EConfigType.TrustTunnel);
        profile.CoreType.Should().Be(ECoreType.sing_box);
        profile.Address.Should().Be("land.gomacondo.com");
        profile.Username.Should().Be("tt_inbound");
        profile.Password.Should().Be("secret-pass");
        profile.StreamSecurity.Should().Be(Global.StreamSecurity);
        profile.Alpn.Should().Be(expectedAlpn);
        profile.Network.Should().BeEmpty();
    }
}
