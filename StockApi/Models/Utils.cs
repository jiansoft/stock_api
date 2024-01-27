namespace StockApi.Models;

internal static class Utils
{
    public static TimeSpan GetNextTimeDiff(int hour)
    {
        var now = DateTime.Now;
        var nextTime = DateTime.Today.AddHours(hour);

        if (now > nextTime)
        {
            nextTime = nextTime.AddDays(1);
        }

        return nextTime - now;
    }
}