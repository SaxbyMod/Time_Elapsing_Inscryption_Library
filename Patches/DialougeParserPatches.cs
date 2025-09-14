using HarmonyLib;
using TimeElapsingInscryption.Util;
using System;

namespace TimeElapsingInscryption.Patches
{
	[HarmonyPatch]
	public class DialougeParserPatches
	{
		[HarmonyPostfix, HarmonyPatch(nameof(DialogueParser.ParseDialogueCodes), typeof(DialogueParser))]
		public static void ParseDialogueCodes(string message, string[] variableStrings = null)
		{
			int num = 0;
			while (message.Length > num)
			{
				string dialogueCode = DialogueParser.GetDialogueCode(message, num);
				if (!string.IsNullOrEmpty(dialogueCode))
				{
					num += dialogueCode.Length;
					string newValue = "";
					if (dialogueCode.StartsWith("[timestamp"))
					{
						(string type, int duration) = GetSoughtTimestampClass.GetSoughtTimestamp(dialogueCode, "timestamp");
						_ = ReverseStopwatch.DownAsync(duration, type, remaining =>
						{
							message = message.Replace(dialogueCode, remaining.ToString());
						});
					}
					message = message.Replace(dialogueCode, newValue);
				}
				else
				{
					num++;
				}
			}
		}
	}
}