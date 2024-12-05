using HarmonyLib;

namespace Triquetra.Input.CustomHandController
{
    [HarmonyPatch(typeof(GloveAnimation), nameof(GloveAnimation.ClearInteractPose))]
    public class Patch_GloveAnimation
    {
        public static bool Prefix(GloveAnimation __instance)
        {
            // clear interact pose is null referencing moment
            return !(__instance is TriquetraInput_GloveAnimation);
        }
    }
    [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.Vibrate))]
    public class Patch_VRHandController
    {
        public static bool Prefix(VRHandController __instance)
        {
            // vibrate null refs moments
            return !(__instance is TriquetraInput_VRHandController);
        }
    }
}