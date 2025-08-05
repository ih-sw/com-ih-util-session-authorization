using com.ih.util.cryptography.Service;
using com.ih.util.session.authorization.Domain;
using com.ih.util.session.authorization.Errors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace com.ih.util.session.authorization.Service;

public interface IAppSessionService
{
    Task<AppSessionDto> Get();
    Task<string> GenerateToken(AppSessionDto appSession);
    string? GetHeaderValue(string headerKey);
    void InitialValidation();
}

public class AppSessionService : IAppSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICryptographyService _cryptographyService;
    private readonly IAppSessionConfigurationService _appSessionConfiguration;

    public AppSessionService(
        IHttpContextAccessor httpContextAccessor,
        ICryptographyService cryptographyService,
        IAppSessionConfigurationService appSessionConfiguration)
    {
        _httpContextAccessor = httpContextAccessor;
        _cryptographyService = cryptographyService;
        _appSessionConfiguration = appSessionConfiguration;
    }

    public async Task<AppSessionDto> Get()
    {
        ValidateCryptographyService();
        
        try
        {
            var headerValue = GetHeaderValue(_appSessionConfiguration.Get().Headers.ApiHeaderTokenName);

            if (!string.IsNullOrEmpty(headerValue))
            {
                var bearerToken = headerValue.Replace("Bearer ", string.Empty);
                var bearerTokenDecode = await _cryptographyService.Decrypt(bearerToken);

                if (!string.IsNullOrEmpty(bearerTokenDecode))
                {
                    var session = JsonConvert.DeserializeObject<AppSessionDto>(bearerTokenDecode);

                    if (session is not null &&
                        session.Data is not null &&
                        session.Data?.Application is not null &&
                        session.Data?.CustomSession is not null &&
                        session.Data?.User is not null &&
                        session.HasSession)
                    {
                        return new AppSessionDto()
                        {
                            HasSession = true,
                            Data = session.Data
                        };
                    }
                }
            }

            return new AppSessionDto()
            {
                HasSession = false
            };
        }
        catch (AppAuthenticationErrorDecodeTokenCustomErrorException e)
        {
            throw e;
        }
    }

    public async Task<string> GenerateToken(AppSessionDto appSession)
    {
        ValidateCryptographyService();
        
        try
        {
            return await _cryptographyService.Encrypt(JsonConvert.SerializeObject(appSession));
        }
        catch (AppAuthenticationErrorGenerateTokenCustomErrorException e)
        {
            e.contentBodyRequest = JsonConvert.SerializeObject(appSession);

            throw e;
        }
    }

    public string? GetHeaderValue(string headerKey)
    {
        try
        {
            return _httpContextAccessor.HttpContext.Request.Headers[headerKey].ToString();
        }
        catch (AppAuthenticationErrorGetHeaderValueCustomErrorException e)
        {
            e.contentBodyRequest = "Header Key: " + headerKey;

            throw e;
        }
    }

    public void InitialValidation()
    {
        try
        {
            var config = _appSessionConfiguration.Get();

            if (config.Headers.ValidateHeaderKeys)
            {
                if (!config.Headers.ApiKeyValue.Equals(GetHeaderValue(config.Headers.ApiKeyName)) ||
                    !config.Headers.ApiSecretValue.Equals(GetHeaderValue(config.Headers.ApiSecretName)))
                {
                    throw new AppAuthenticationSignatureNotFoundCustomErrorException();
                }
            }

            var headerValue = GetHeaderValue(config.Headers.ApiHeaderTokenName);

            if (!string.IsNullOrEmpty(headerValue))
            {
                if (!headerValue.Contains("Bearer "))
                {
                    throw new AppAuthenticationInvalidTokenCustomErrorException();
                }
            }

            throw new AppAuthenticationTokenNotFoundCustomErrorException();
        }
        catch (AppAuthenticationInvalidTokenCustomErrorException e)
        {
            throw e;
        }
        catch (AppAuthenticationTokenNotFoundCustomErrorException e)
        {
            throw e;
        }
        catch (AppAuthenticationSignatureNotFoundCustomErrorException e)
        {
            throw e;
        }
    }

    private void ValidateCryptographyService()
    {
        if (_cryptographyService is null)
        {
            throw new AppAuthenticationCryptographyServiceNotInitializedCustomErrorException();
        }
    }
}