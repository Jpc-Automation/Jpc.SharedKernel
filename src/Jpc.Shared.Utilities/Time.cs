namespace Infrastructure;

public static class Time
{
    private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static DateTime UnixMillisecondsToDateTimeUtc(double unixTime)
        => epoch.AddMilliseconds(unixTime);

    public static DateTime UnixSecondsToDateTimeUtc(double unixTime)
        => epoch.AddSeconds(unixTime);

    public static DateTime UnixMillisecondsToDateTimeToLocal(double unixTime)
        => UnixMillisecondsToDateTimeUtc(unixTime).ToLocalTime();

    public static DateTime UnixSecondsToDateTimeToLocal(double unixTime)
        => UnixSecondsToDateTimeUtc(unixTime).ToLocalTime();
}
