using HarmonyLib;
using UnityEngine;
using static CameraZoom.MainMod;

namespace CameraZoom;

[HarmonyPatch(typeof(CameraController), "ClampZoomLevel")]
internal static class CameraController_ClampZoomLevel {
    internal static void Postfix(Vector2 confinerSize, float zoomLevel, ref float __result) {
        // The zoom is clamped before is it applied from a CameraZoomController
        // to the CameraController. Therefore, I can just apply the multiplier
        // here.
        if (is_zoom_constant) zoomLevel = 1f;
        zoomLevel *= camera_zoom_multiplier;

        if (is_zoom_forced) {
            __result = zoomLevel;
            return;
        }

        // Take the max. Black borders horizontal or vertical are fine. Diagonal
        // ones are bad because then you can see some random textures in the
        // corners.
        if (confinerSize.x > 1f || confinerSize.y > 1f) {
            float b = Mathf.Max(confinerSize.x, confinerSize.y);
            __result = Mathf.Min(zoomLevel, b);
            return;
        }
    }
}
