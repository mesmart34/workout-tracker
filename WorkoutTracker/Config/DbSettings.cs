namespace WorkoutTracker.Config;

public class DbSettings
{
    public const string ConfigSection = "DatabaseSettings";
    public string? Endpoint { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string ConnectionString()
    {   
        //return $"Host=localhost;Port=5432;Database=workout;Username=postgres;Password=postgres;SslMode=Disable";
        return $"Host={Endpoint};Port={Port};Database={Database};Username={Username};Password={Password};SslMode=Disable;";
    }
}