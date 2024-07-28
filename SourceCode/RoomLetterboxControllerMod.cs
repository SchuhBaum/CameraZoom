using HarmonyLib;
using UnityEngine;
using static CameraZoom.MainMod;

namespace CameraZoom;

[HarmonyPatch(typeof(RoomLetterboxController), "InitializeLetterbox")]
internal static class RoomLetterboxController_InitializeLetterbox {
	// private enum LetterboxSide { Left, Right, Top, Bottom }
    internal static void Postfix(int side, BaseRoom room, ref GameObject[] ___m_letterBoxArray) {
        // In vanilla these don't reach into the corners when zooming out small
        // rooms. Then you see the background and clouds and stuff in the corners.
        GameObject letterbox = ___m_letterBoxArray[side];
        Bounds bounds = room.Collider2D.bounds;
        Vector3 local_scale = new();

        // left and right
        if (side < 2) {
            local_scale.x = 10f;
            local_scale.y = camera_zoom_multiplier * (bounds.size.y + 2f) / 2f;
            local_scale.z = 1f;
            letterbox.transform.localScale = local_scale;
            return;
        }

        // top and bottom
        local_scale.x = camera_zoom_multiplier * (bounds.size.x + 2f) / 2f;
        local_scale.y = 10f;
        local_scale.z = 1f;
        letterbox.transform.localScale = local_scale;
    }
}
