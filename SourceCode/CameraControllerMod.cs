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

        // On my 16:10 screen I have the problem that the game zooms in. So it
        // scrolls in one-screen wide rooms. Undo this here.
        if (!AspectRatioManager.ForceEnable_16_9 && !AspectRatioManager.IsScreen_16_9_AspectRatio) {
            float ratio = AspectRatioManager.STANDARD_16_9_ASPECT_RATIO / AspectRatioManager.CurrentGameAspectRatio;
            if (ratio < 1f) ratio = 1f;
            confinerSize *= ratio;
            zoomLevel    *= ratio;
        }

        if (is_zoom_forced) {
            __result = zoomLevel;
            return;
        }

        if (confinerSize.x > 1f && confinerSize.y > 1f) {
            float b = Mathf.Min(confinerSize.x, confinerSize.y);
            __result = Mathf.Min(zoomLevel, b);
            return;
        }
    }
}
