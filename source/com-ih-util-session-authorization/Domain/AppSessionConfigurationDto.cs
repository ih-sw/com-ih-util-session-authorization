namespace com.ih.util.session.authorization.Domain;

public class AppSessionConfigurationDto
{
    public ApplicationSessionConfigurationApplicationDto Application { get; set; }
    public ApplicationSessionConfigurationHeadersDto Headers { get; set; }
}

public class ApplicationSessionConfigurationHeadersDto
{
    public bool ValidateHeaderKeys { get; set; }

    public string ApiHeaderTokenName { get; set; } = string.Empty;

    public string ApiKeyName { get; set; } = string.Empty;
    public string ApiSecretName { get; set; } = string.Empty;
    
    public string ApiKeyValue { get; set; } = string.Empty;
    public string ApiSecretValue { get; set; } = string.Empty;
}

public class ApplicationSessionConfigurationApplicationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}