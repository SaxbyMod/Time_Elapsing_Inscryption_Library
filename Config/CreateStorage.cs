using BepInEx.Configuration;

namespace TimeElapsingInscryption.Config
{
	public class CreateStorage
	{
		public static ConfigFile Config;
        
		// Config Vars
		public static ConfigEntry<string> storedCountdowns;
        
		public static void Init()
		{
			storedCountdowns = Config.Bind<string>("DON'T TOUCH SECTION",
				$"DO NOT TOUCH UNLESS YOU KNOW WHAT YOUR DOING!",
				"",
				"Stores a list associated with the libraries countdowns.");
		}
	}
}