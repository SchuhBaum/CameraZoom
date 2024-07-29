using HarmonyLib;
using UnityEngine;

namespace CameraZoom;

[HarmonyPatch(typeof(RelicRoomPropController), "InitializeTextBox")]
internal static class RelicRoomPropController_InitializeTextBox {
    internal static void Postfix(bool leftSide, RelicRoomPropController __instance) {
        GenericInfoTextBox genericInfoTextBox;
		if (leftSide) {
            genericInfoTextBox = __instance.LeftInfoTextBox;
		} else {
            genericInfoTextBox = __instance.RightInfoTextBox;
		}

        Transform? transform = genericInfoTextBox?.transform.Find("BG");
        if (transform != null) {
            float zoom_level = Mathf.Min(1.5f, CameraController.ZoomLevel);
            transform.localScale = new Vector3(zoom_level, zoom_level, 1f);
        }
    }
}
