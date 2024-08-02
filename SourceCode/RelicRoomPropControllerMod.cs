using HarmonyLib;
using static CameraZoom.MainMod;

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
        zoom_info_text_box(genericInfoTextBox);
    }
}

[HarmonyPatch(typeof(HealingRoomPropController), "InitializePooledPropOnEnter")]
internal static class HealingRoomPropController_InitializePooledPropOnEnter {
    internal static void Postfix(HealingRoomPropController __instance) {
        zoom_info_text_box(__instance.LeftInfoTextBox);
        zoom_info_text_box(__instance.RightInfoTextBox);
    }
}
