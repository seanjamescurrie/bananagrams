using Bananagrams.Api.Extensions;

namespace Bananagrams.Api;

public static class EnvironmentVariables
{
    private static string DbConnectionStringKey => "DbConnectionString";

    public static string DbConnectionString => DbConnectionStringKey.GetValue("Server=localhost;Port=5432;Database=postgres;User Id=user;Password=password;");
}