using BepInEx;
using UnityEngine;
using Wob_Common;

namespace CameraZoom;

[BepInPlugin("SchuhBaum.CameraZoom", "CameraZoom", "0.0.3")]
public class MainMod : BaseUnityPlugin {
    // meta data
    public static string author = "SchuhBaum";
    public static string mod_id = "CameraZoom";
    public static string version = "v0.0.3";

    // options
    public static float camera_zoom_multiplier = 1.32f;
    public static bool is_zoom_constant        = false;
    public static bool is_zoom_forced          = true;

	// variables
    public static bool is_initialized = false;

    //
    // main
    //

    protected void Awake() {
        if (is_initialized) return;
        is_initialized = true;
        WobPlugin.Initialise(this, this.Logger);

        WobSettings.Add(new WobSettings.Num<float>("Options", "camera_zoom_multiplier", "Values larger than 1 zoom out the camera. The camera does not zoom out if the room is too small.", camera_zoom_multiplier, bounds: (0.1f, 2.5f)));
        WobSettings.Add(new WobSettings.Boolean("Options", "is_zoom_constant", "When enabled, the zoom does not changed based on room or biome specific rules. The base zoom value stays at 1.", is_zoom_constant));
        WobSettings.Add(new WobSettings.Boolean("Options", "is_zoom_forced", "When enabled, the zoom is used in small rooms as well. This creates black borders.", is_zoom_forced));
        WobPlugin.Patch();

        camera_zoom_multiplier = WobSettings.Get("Options", "camera_zoom_multiplier", camera_zoom_multiplier);
        is_zoom_constant       = WobSettings.Get("Options", "is_zoom_constant", is_zoom_constant);
        is_zoom_forced         = WobSettings.Get("Options", "is_zoom_forced", is_zoom_forced);

        Debug.Log(mod_id + ": author "                 + author);
        Debug.Log(mod_id + ": version "                + version);
        Debug.Log(mod_id + ": camera_zoom_multiplier " + camera_zoom_multiplier);
        Debug.Log(mod_id + ": is_zoom_constant "       + is_zoom_constant);
        Debug.Log(mod_id + ": is_zoom_forced "         + is_zoom_forced);
    }

    //
    // public
    //

    public static void zoom_info_text_box(GenericInfoTextBox? info_text_box) {
        // If you zoom too far out it overlaps with the props in the foreground.
        Transform? transform = info_text_box?.transform.Find("BG");
        if (transform == null) return;
        float zoom_level = Mathf.Min(1.5f, CameraController.ZoomLevel);
        transform.localScale = new Vector3(zoom_level, zoom_level, 1f);
    }
}
