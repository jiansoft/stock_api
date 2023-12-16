namespace StockApi.Models;

public static class Utils
{
    public static TimeSpan GetNextTimeDiff(int hour)
    {
        var now = DateTime.Now;
        var nextTime = new DateTime(now.Year, now.Month, now.Day, hour, 0, 0);

        if (now > nextTime)
        {
            nextTime = nextTime.AddDays(1);
        }

        var timeDifference = nextTime - now;

        return timeDifference;
    }
}