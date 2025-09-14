using AnsiConsolePlugin.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace TimeElapsingInscryption.Util
{
	public class GetSoughtTimestampClass
	{
		public static (string, int) GetSoughtTimestamp(string dialougeCode, string ID)
		{
			dialougeCode = dialougeCode.Replace("[timestamp:", "").Replace("]", "");
			List<string> codePieces = dialougeCode.Split(';').ToList();
			int Length = 0;
			string Type = "Nan";
			if (codePieces.Count == 1)
			{
				Console.Write(GetColorFromTypeFunctions.GetColorFromString("Red", "Highlighted") + "ERROR; Not enough pieces for the timestamp to register, it requires 2 pieces split by a ';'." + ANSICodeLists.ResetColor);
			}
			else
			{
				try
				{
					Length = int.Parse(codePieces[0]);
				}
				catch
				{
					Console.Write(GetColorFromTypeFunctions.GetColorFromString("Red", "Highlighted") + "ERROR; The first piece must be a valid integer." + ANSICodeLists.ResetColor);
					Type = Conversion.SwitchType(Type);
				}
			}
			
			return (Type, Length);
		}
	}
}