using FakeItEasy;
using Microsoft.Extensions.Options;
using OpenID4VC_Prototype.Configurations;
using OpenID4VC_Prototype.Services;

namespace OpenId4VC_Tests.Services;
public class DIdServiceTests
{
    [Fact]
    public void GenerateDId_ShouldReturnValidDId()
    {
        // Arrange
        var didSettings = new DIdConfiguration()
        {
            DefaultKeySize = 2048,
            DIdPrefix = "did:example:"
        };

        var fakeOptions = A.Fake<IOptions<DIdConfiguration>>();

        A.CallTo(() => fakeOptions.Value).Returns(didSettings);

        var dIdService = new DIdService(fakeOptions);

        // Act
        var dId = dIdService.GenerateDId();

        // Assert
        Assert.StartsWith("did:example:", dId.DId);
        Assert.InRange(dId.DId.Length, 48, 48);
    }
}
