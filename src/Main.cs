using System;
using System.Reflection;
using Rage;
using Rage.Attributes;

[assembly: Rage.Attributes.Plugin("TimeNotifier", Description = "A plugin that notifies the player about the current time at regular intervals.", Author = "")]

namespace TimeNotifier
{
    public static class EntryPoint
    {
        public const string AUTHOR = "";
        public const bool TimeNotificationsEnabled = true;
        public const bool TwelveHourClockEnabled = true;
        public const int SecondsPerNotification = 30; // 30 seconds
        public const string TimeNotificationText = "~b~TimeNotifier:~s~ The current time is ~b~{0}~s~.";
        public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly string PluginName = typeof(EntryPoint).Namespace.ToString();

        public static void Main()
        {
            Game.LogTrivial(string.Format("Plugin {0} v{1} by {2} has been initialized.",
                PluginName, Version, AUTHOR));
            Game.DisplayNotification(string.Format("~b~{0}~s~ v{1} by ~g~{2}~s~ has been loaded.",
                PluginName, Version, AUTHOR));
            TimeNotifLoop();
        }

        public static void TimeNotifLoop()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                DateTime now = DateTime.Now;
                TimeSpan span = now - startTime;
                int secondsSinceMidnight = Convert.ToInt32((now - DateTime.Today).TotalSeconds);
                int totalSpanSeconds = Convert.ToInt32(span.TotalSeconds);

                string clockTime = TwelveHourClockEnabled ? now.ToString("hh:mm tt") : now.ToString("HH:mm");

                if (TimeNotificationsEnabled && (secondsSinceMidnight % SecondsPerNotification == 0))
                {
                    Game.DisplayNotification(string.Format(TimeNotificationText, clockTime));
                    Game.LogTrivial("Notified the player about the current time: " + clockTime);
                }

                GameFiber.Sleep(1000);
            }
        }
    }
}
