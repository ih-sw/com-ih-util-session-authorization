namespace com.ih.session.authorization.Domain;

public class AppSessionDto
{
    public bool HasSession { get; set; }

    public SessionDataDto? Data { get; set; }
}

public class SessionDataDto
{
    public SessionApplicationDto Application { get; set; }
    public SessionUserDto User { get; set; }
    public SessionCustomSessionDto CustomSession { get; set; }

    public List<string> Claims { get; set; } = new();
}

public class SessionCustomSessionDto
{
    public Guid Id { get; set; }
    public long ReferenceCode { get; set; }

    public DateTime LoggedAt { get; set; }
    public DateTime? ExpirationAt { get; set; }
}

public class SessionUserDto
{
    public Guid Id { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsSuperUser { get; set; }
}

public class SessionApplicationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}