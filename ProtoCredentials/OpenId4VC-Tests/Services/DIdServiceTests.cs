using Microsoft.Extensions.Options;
using Moq;
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

        var mockOptions = new Mock<IOptions<DIdConfiguration>>();
        mockOptions.Setup(o => o.Value).Returns(didSettings);

        var dIdService = new DIdService(mockOptions.Object);

        // Act
        var dId = dIdService.GenerateDId();

        // Assert
        Assert.StartsWith("did:example:", dId.DId);
        Assert.InRange(dId.DId.Length, 48, 48);
    }
}
