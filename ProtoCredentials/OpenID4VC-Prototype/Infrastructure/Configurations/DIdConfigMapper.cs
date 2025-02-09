using domainDIdConfig = OpenID4VC_Prototype.Domain.Models.DIdConfig;

namespace OpenID4VC_Prototype.Infrastructure.Configurations;
public class DIdConfigMapper
{
    public static domainDIdConfig ToDomainModel(DIdConfig configuration)
    {
        return new domainDIdConfig
        {
            DIdPrefix = configuration.DIdPrefix,
            DefaultKeySize = configuration.DefaultKeySize
        };
    }
}
