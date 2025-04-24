using System.Net.NetworkInformation;

namespace Jpc.Shared.Utilities;

public static class Ping
{
    public static PingResult PingHost(string nameOrAddress, int timeout = 5000)
    {
        try
        {
            using var pinger = new System.Net.NetworkInformation.Ping();
            var reply = pinger.Send(nameOrAddress, timeout);
            return PingResult.FromReply(reply);
        }
        catch (PingException ex)
        {
            return PingResult.FromException(ex);
        }
    }

    public static Task<PingResult> PingHostAsync(string nameOrAddress, int timeout = 5000)
    {
        var task = Task.Run(() =>
        {
            try
            {
                using var pinger = new System.Net.NetworkInformation.Ping();
                var reply = pinger.Send(nameOrAddress, timeout);
                return Task.FromResult(PingResult.FromReply(reply));
            }
            catch (PingException ex)
            {
                return Task.FromResult(PingResult.FromException(ex));
            }
        });

        return task;
    }

    public sealed record PingResult
    {
        public IPStatus Status { get; init; }
        public bool IsSuccess { get; init; }
        public long TimeMs { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;

        public static PingResult FromReply(PingReply reply)
        {
            var result = new PingResult
            {
                IsSuccess = reply.Status == IPStatus.Success,
                Status = reply.Status,
                TimeMs = reply.RoundtripTime
            };

            return result;
        }

        public static PingResult FromException(PingException ex)
        {
            var result = new PingResult
            {
                IsSuccess = false,
                Status = IPStatus.Unknown,
                ErrorMessage = ex.InnerException?.Message ?? string.Empty
            };

            return result;
        }
    }

}
