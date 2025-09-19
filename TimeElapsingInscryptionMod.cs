using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TimeElapsingInscryption.Config;
using TimeElapsingInscryption.Patches;
using TimeElapsingInscryption.Util.HandleStampReturns;

namespace TimeElapsingInscryption
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class TimeElapsingInscryptionMod : BaseUnityPlugin
    {
        public const string PluginGuid = "creator.BepInEx.TimeElapsingInscryptionMod";
        public const string PluginName = "Time Elapsing Inscryption Mod";
        public const string PluginVersion = "1.0.0";

        // Define a Manual Log Source
        public static ManualLogSource Log = new ManualLogSource(PluginName);
        public static Harmony harmony = new Harmony(PluginGuid);

        public void Awake()
        {
            harmony.PatchAll(typeof(DialougeParserPatches));
            CreateStorage.Config = Config;
            CreateStorage.Init();
            BasicStampTimeUnits.InstantiateTimeHolders();
        }
    }
}