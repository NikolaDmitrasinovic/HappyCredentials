using OpenID4VC_Prototype.Domain.Models;
using OpenID4VC_Prototype.Domain.Services;

namespace OpenId4VC_Tests.Services;
public class DIdServiceTests
{
    [Fact]
    public void GenerateDId_ShouldReturnValidDId()
    {
        // Arrange
        var dIdSettings = new DIdConfig()
        {
            DefaultKeySize = 2048,
            DIdPrefix = "did:example:"
        };

        var mockDIdService = new DIdService(dIdSettings);

        // Act
        var dId = mockDIdService.GenerateDId();

        // Assert
        Assert.StartsWith("did:example:", dId.DId);
        Assert.InRange(dId.DId.Length, 48, 48);
    }
}
