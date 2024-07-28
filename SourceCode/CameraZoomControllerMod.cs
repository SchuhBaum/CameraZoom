using HarmonyLib;
using static CameraZoom.MainMod;

[HarmonyPatch(typeof(CameraZoomController), "SetZoomLevel")]
internal static class CameraController_SetZoomLevel {
    internal static bool Prefix() {
        // This function is used to override the camera zoom based on biome rules.
        return !is_zoom_constant;
    }
}
