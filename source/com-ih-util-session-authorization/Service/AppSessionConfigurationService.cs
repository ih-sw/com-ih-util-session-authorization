using com.ih.util.session.authorization.Domain;

namespace com.ih.util.session.authorization.Service;

public interface IAppSessionConfigurationService
{
    AppSessionConfigurationDto Get();
}

public class AppSessionConfigurationService : IAppSessionConfigurationService
{
    private readonly AppSessionConfigurationDto _appSessionConfiguration;

    public AppSessionConfigurationService(AppSessionConfigurationDto appSessionConfiguration)
    {
        this._appSessionConfiguration = appSessionConfiguration;
    }

    public AppSessionConfigurationDto Get()
    {
        return _appSessionConfiguration;
    }
}