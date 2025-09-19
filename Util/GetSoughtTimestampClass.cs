using AnsiConsolePlugin.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeElapsingInscryption.Patches;
using TimeElapsingInscryption.Util.HandleStampReturns;

namespace TimeElapsingInscryption.Util
{
	public class GetSoughtTimestampClass
	{
		public static string GetSoughtTimestamp(string dialogueCode, string ID)
		{
			if (!dialogueCode.StartsWith($"[{DialougeParserPatches.TimestampCodeBase}"))
				return dialogueCode;
			
			dialogueCode = dialogueCode.Replace("[", "").Replace("]", "").Substring(DialougeParserPatches.TimestampCodeBase.Length);
			var splitMessage = dialogueCode.Split(':');
			string msg = "";
			if (splitMessage.Length >= 3)
			{
				var type = splitMessage[1];
				var time = splitMessage[2];
				
				// Printed String;
				if (type == "Seconds" || type == "Hours" || type == "Minutes" || type == "Ticks" || type == "Days")
				{
					msg = BasicStampTimeUnits.Initialize(type, time);
				}
			}
			
			return msg;
		}
	}
}