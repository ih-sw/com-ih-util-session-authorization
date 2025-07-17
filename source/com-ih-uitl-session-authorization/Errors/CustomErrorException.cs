using IH.CustomErrors.Util.Domain;
using Microsoft.AspNetCore.Http;

namespace com.ih.session.authorization.Errors;

[Serializable]
public class AppAuthenticationExpiredSessionCustomErrorException : CustomErrorException
{
    public override string message { get { return "Expired Session"; } }
    public override string code { get { return "authentication-expired-session"; } }
    public override int status { get { return StatusCodes.Status401Unauthorized; } }

    public AppAuthenticationExpiredSessionCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationForbiddenCustomException : CustomErrorException
{
    public override string message { get { return "Forbidden Access"; } }
    public override string code { get { return "authentication-forbidden"; } }
    public override int status { get { return StatusCodes.Status403Forbidden; } }

    public AppAuthenticationForbiddenCustomException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationInvalidTokenCustomErrorException : CustomErrorException
{
    public override string message { get { return "Authenticaton Invalid Token"; } }
    public override string code { get { return "authentication_invalid_token"; } }
    public override int status { get { return StatusCodes.Status401Unauthorized; } }

    public AppAuthenticationInvalidTokenCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationSignatureNotFoundCustomErrorException : CustomErrorException
{
    public override string message { get { return "Keys For Security Not Found"; } }
    public override string code { get { return "authentication_signature_not_found"; } }
    public override int status { get { return StatusCodes.Status401Unauthorized; } }

    public AppAuthenticationSignatureNotFoundCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationTokenNotFoundCustomErrorException : CustomErrorException
{
    public override string message { get { return "Token Not Found"; } }
    public override string code { get { return "authentication_token_not_found"; } }
    public override int status { get { return StatusCodes.Status401Unauthorized; } }

    public AppAuthenticationTokenNotFoundCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationErrorDecodeTokenCustomErrorException : CustomErrorException
{
    public override string message { get { return "Error In Decode Token"; } }
    public override string code { get { return "authentication_error_decode_token"; } }
    public override int status { get { return StatusCodes.Status503ServiceUnavailable; } }

    public AppAuthenticationErrorDecodeTokenCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationErrorGenerateTokenCustomErrorException : CustomErrorException
{
    public override string message { get { return "Error In Generate Token"; } }
    public override string code { get { return "authentication_error_generate_token"; } }
    public override int status { get { return StatusCodes.Status503ServiceUnavailable; } }

    public AppAuthenticationErrorGenerateTokenCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationErrorGetHeaderValueCustomErrorException : CustomErrorException
{
    public override string message { get { return "Error In Get Header Value"; } }
    public override string code { get { return "authentication_error_get_header_value"; } }
    public override int status { get { return StatusCodes.Status503ServiceUnavailable; } }

    public AppAuthenticationErrorGetHeaderValueCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}

[Serializable]
public class AppAuthenticationCryptographyServiceNotInitializedCustomErrorException : CustomErrorException
{
    public override string message { get { return "Cryptography Service Not Initialized"; } }
    public override string code { get { return "authentication_error_cryptography_service_not_initialized"; } }
    public override int status { get { return StatusCodes.Status424FailedDependency; } }

    public AppAuthenticationCryptographyServiceNotInitializedCustomErrorException(string? contentBodyRequest = null, string? contentBodyResponse = null)
    {
        this.contentBodyRequest = contentBodyRequest;
        this.contentBodyResponse = contentBodyResponse;
    }
}