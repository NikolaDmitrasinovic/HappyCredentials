﻿using FakeItEasy;
using Microsoft.Extensions.Options;
using OpenID4VC_Prototype.Infrastructure.Configurations;
using OpenID4VC_Prototype.Services;

namespace OpenId4VC_Tests.Services;
public class DIdServiceTests
{
    [Fact]
    public void GenerateDId_ShouldReturnValidDId()
    {
        // Arrange
        var didSettings = new DIdConfig()
        {
            DefaultKeySize = 2048,
            DIdPrefix = "did:example:"
        };

        var fakeOptions = A.Fake<IOptions<DIdConfig>>();

        A.CallTo(() => fakeOptions.Value).Returns(didSettings);

        var mockDIdService = new DIdService(fakeOptions);

        // Act
        var dId = mockDIdService.GenerateDId();

        // Assert
        Assert.StartsWith("did:example:", dId.DId);
        Assert.InRange(dId.DId.Length, 48, 48);
    }
}
