using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Infrastructure.Configurations;
public class DIdConfigMapper
{
    public static DIdConfig ToDomainModel(DIdConfiguration configuration)
    {
        return new DIdConfig
        {
            DIdPrefix = configuration.DIdPrefix,
            DefaultKeySize = configuration.DefaultKeySize
        };
    }
}
