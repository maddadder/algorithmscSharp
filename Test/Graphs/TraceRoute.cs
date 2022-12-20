using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

public static class TraceRoute
{
    public static Tuple<IPAddress,IPAddress> GetTraceRoute(string hostNameOrAddress, int ttl, List<Tuple<IPAddress,IPAddress>> result)
    {
        Ping pinger = new Ping();
        PingOptions pingerOptions = new PingOptions(ttl, true);
        int timeout = 10000;
        byte[] buffer = Array.Empty<byte>();

        var reply = pinger.Send(hostNameOrAddress, timeout, buffer, pingerOptions);

        if (reply.Status == IPStatus.Success)
        {
            return new Tuple<IPAddress,IPAddress>(result.LastOrDefault()?.Item2,reply.Address);
        }
        else if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut || reply.Status == IPStatus.TimeExceeded)
        {
            //add the currently returned address if an address was found with this TTL
            if (reply.Status == IPStatus.TtlExpired) 
                return new Tuple<IPAddress,IPAddress>(result.LastOrDefault()?.Item2,reply.Address);
            //recurse to get the next address...
            var tmp = GetTraceRoute(hostNameOrAddress, ttl + 1, result);
            result.Add(tmp);
        }
        else
        {
            //failure 
        }

        return new Tuple<IPAddress,IPAddress>(result.LastOrDefault()?.Item2,reply.Address);
    }
}