using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeElapsingConsolePlugin.Util;
using TimeElapsingInscryption.Config;

namespace TimeElapsingInscryption.Util.HandleStampReturns
{
	public class BasicStampTimeUnits
	{
		public static Dictionary<string, int> TimeHolders = new Dictionary<string, int>();
		private static readonly object ConfigAndDictLock = new object();
		
		public static void InstantiateTimeHolders()
		{
			List<string> savedCountdowns = CreateStorage.storedCountdowns.Value.Split('|').ToList();
			int CurrentPlace = 0;
			if (savedCountdowns.Count > TimeHolders.Count)
			{
				foreach (string item in savedCountdowns)
				{
					List<string> countdownVar = item.Split(':').ToList();
					int num = int.Parse(countdownVar[0]);
					string parsedCountdown = countdownVar[1].TrimStart().Replace("{{", "").Replace("}}", "");
					List<string> countpieces = parsedCountdown.Replace(" ", "").Split(',').ToList();
					lock (ConfigAndDictLock)
					{
						TimeHolders.Add(countpieces[0], int.Parse(countpieces[1]));
					}
				}
			}
		}

		public static string Initialize(string Type, string timespan)
		{
			Information info = StartDown(Type, timespan);
			if (TimeHolders.TryGetValue(info.ModName, out int remaining))
				return remaining + " " + Type;
			else
				return timespan + " " + Type;
		}

		public static Information StartDown(string Type, string timespan)
		{
			int count = GetCurrentNum();
			var Info = new Information(timespan, Type, "Timestamp Inscryption Library" + count);
			lock (ConfigAndDictLock)
			{
				CreateStorage.storedCountdowns.Value += $"{count}: {{{Info.ModName}, {timespan}}}|";
			}
			int previousSpan = int.Parse(timespan);
			Info.Run?.Cancel();
			Info.Run?.Dispose();
			Info.Run = new CancellationTokenSource();
			Stopwatches.HandleStopwatch("Reset", Info.watch);
			Stopwatches.HandleStopwatch("Start", Info.watch);
			Task.Run(async () =>
				{
					while (!Info.Run.IsCancellationRequested)
					{
						int AwaitedTime = Stopwatches.GetTimeFromType(Info.Type, Stopwatches.HandleStopwatch("Elapsed", Info.watch));
						int remaining = Math.Max(int.Parse(timespan) - AwaitedTime, 0);
						InsertOrModifyHolders(remaining, Info, previousSpan, count);
						previousSpan = remaining;
						if (remaining <= 0)
						{
							StopDown(Info);
							InsertOrModifyHolders(0, Info, previousSpan, count);
							break;
						}
						await Task.Delay(1000, Info.Run.Token);
					}
				}
			);
			return Info;
		}
		
		public static void StopDown (Information Info)
		{
			if (Info.Run == null)
			{
				return;
			}
			Info.Run.Cancel();
			Stopwatches.HandleStopwatch("Stop", Info.watch);
		}
		
		
		public static void InsertOrModifyHolders(int Time, Information Info, int previousSpan, int count)
		{
			lock (ConfigAndDictLock)
			{
				TimeHolders[Info.ModName] = Time;
				if (Time <= 0)
				{
					CreateStorage.storedCountdowns.Value = CreateStorage.storedCountdowns.Value.Replace($"{count}: {{{Info.ModName}, {previousSpan}}}|", "");
				}
				else
				{
					CreateStorage.storedCountdowns.Value = CreateStorage.storedCountdowns.Value.Replace($"{count}: {{{Info.ModName}, {previousSpan}}}|", $"{count}: {{{Info.ModName}, {Time}}}|");
				}
			}
		}

		public static int GetCurrentNum()
		{
			List<string> savedCountdowns = CreateStorage.storedCountdowns.Value.Split('|').ToList();
			int CurrentPlace = 0;
			if (savedCountdowns.Count < TimeHolders.Count)
			{
				foreach (string item in savedCountdowns)
				{
					List<string> countdownVar = item.Split(':').ToList();
					int num = int.Parse(countdownVar[0]);
					if (CurrentPlace + 1 == num)
					{
						CurrentPlace = num;
					}
					else
					{
						return TimeHolders.Count;
					}
				}
			}
			return TimeHolders.Count + 1;
		}
	}
}