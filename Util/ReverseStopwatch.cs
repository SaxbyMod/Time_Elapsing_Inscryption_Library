using System;
using System.Threading.Tasks;
namespace TimeElapsingInscryption.Util
{
	public class ReverseStopwatch
	{
		public static async Task<int> DownAsync(int countdown, string type, Action<int> onUpdate)
		{
			TimeSpan span = type switch
			{
				"Milliseconds" => TimeSpan.FromMilliseconds(countdown),
				"Seconds" => TimeSpan.FromSeconds(countdown),
				"Minutes" => TimeSpan.FromMinutes(countdown),
				"Hours" => TimeSpan.FromHours(countdown),
				"Days"  => TimeSpan.FromDays(countdown),
				"Ticks" => TimeSpan.FromTicks(countdown),
				_ => TimeSpan.FromSeconds(countdown)
			};

			DateTime endTime = DateTime.Now.Add(span);

			while (DateTime.Now < endTime)
			{
				int ReturnForm = type switch
				{
					"Milliseconds" => (int)(endTime - DateTime.Now).TotalMilliseconds,
					"Seconds" => (int)(endTime - DateTime.Now).TotalSeconds,
					"Minutes" => (int)(endTime - DateTime.Now).TotalMinutes,
					"Hours" => (int)(endTime - DateTime.Now).TotalHours,
					"Days" => (int)(endTime - DateTime.Now).TotalDays,
					"Ticks" => (int)(endTime - DateTime.Now).Ticks,
					_ => (int)(endTime - DateTime.Now).TotalSeconds
				};
				
				
				var remaining = ReturnForm;
				onUpdate?.Invoke(remaining);
				await Task.Delay(1000);
			}

			onUpdate?.Invoke(0);
			return 0;
		}
	}
}