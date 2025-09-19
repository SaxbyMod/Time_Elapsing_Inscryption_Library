namespace TimeElapsingInscryption.Util
{
	public class Conversion
	{
		public static string SwitchType(string Type)
		{
			switch (Type.ToLower())
			{
				case "s":
				case "second":
				case "seconds":
					Type = "Seconds";
					break;
				case "m":
				case "minute":
				case "minutes":
					Type = "Minutes";
					break;
				case "h":
				case "hour":
				case "hours":
					Type = "Hours";
					break;
				case "d":
				case "day":
				case "days":
					Type = "Days";
					break;
				case "t":
				case "tick":
				case "ticks":
					Type = "Ticks";
					break;
				case "tu":
				case "turn":
				case "turns":
					Type = "Turns";
					break;
				case "p":
				case "phase":
				case "phases":
					Type = "Phases";
					break;
				case "r":
				case "ring":
				case "rings":
					Type = "Rings";
					break;
				case "n":
				case "node":
				case "nodes":
					Type = "Nodes";
					break;
			}
			return Type;
		}
	}
}