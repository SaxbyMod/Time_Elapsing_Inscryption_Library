using HarmonyLib;
using TimeElapsingInscryption.Util;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System.Reflection;

namespace TimeElapsingInscryption.Patches
{
	[HarmonyPatch]
	public class DialougeParserPatches
	{
		// [HarmonyPostfix, HarmonyPatch(nameof(DialogueParser.ParseDialogueCodes), typeof(DialogueParser))]
		// public static void ParseDialogueCodes(string message, string[] variableStrings = null)
		// {
		// 	int num = 0;
		// 	while (message.Length > num)
		// 	{
		// 		string dialogueCode = DialogueParser.GetDialogueCode(message, num);
		// 		if (!string.IsNullOrEmpty(dialogueCode))
		// 		{
		// 			num += dialogueCode.Length;
		// 			string newValue = "";
		// 			if (dialogueCode.StartsWith("[timestamp"))
		// 			{
		// 				(string type, int duration) = GetSoughtTimestampClass.GetSoughtTimestamp(dialogueCode, "timestamp");
		// 				_ = ReverseStopwatch.DownAsync(duration, type, remaining =>
		// 				{
		// 					message = message.Replace(dialogueCode, remaining.ToString());
		// 				});
		// 			}
		// 			message = message.Replace(dialogueCode, newValue);
		// 		}
		// 		else
		// 		{
		// 			num++;
		// 		}
		// 	}
		// }
		
		public static MethodInfo isb_di = AccessTools.Method(typeof(DialougeParserPatches), nameof(GetSoughtTimestampClass.GetSoughtTimestamp));
		public const string TimestampCodeBase = "timestamp";
		
		[HarmonyPatch(typeof(DialogueParser), nameof(DialogueParser.ParseDialogueCodes))]
		[HarmonyILManipulator]
		public static void InsertStampBrackets_Transpiler(ILContext ctx)
		{
			var crs = new ILCursor(ctx);

			if (!crs.JumpBeforeNext(x => x.MatchStloc(4)))
				return;

			crs.Emit(OpCodes.Ldloc_3);
			crs.Emit(OpCodes.Call, isb_di);
		}
	}
}