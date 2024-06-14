using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisableDynamicBone
{
    public class DisableDynamicBone: ResoniteMod
    {
        public override string Name => "DisableDynamicBone";
        public override string Author => "kokoa";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/rassi0429/DisableDynamicBone/";

        internal static ModConfiguration config;

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> disableDynamicBone = new ModConfigurationKey<bool>("disable", "disable dynamic bone", () => false);


        public override void OnEngineInit()
        {
            config = GetConfiguration();
            Harmony harmony = new Harmony("com.kokoa.DisableDynamicBone");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(FrooxEngine.DynamicBoneChainManager), "Update", new Type[] { })]
        class Patch
        {
            static bool Prefix()
            {
                if (config.GetValue(disableDynamicBone))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
