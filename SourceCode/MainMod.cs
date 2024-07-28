using BepInEx;
using UnityEngine;
using Wob_Common;

namespace CameraZoom;

[BepInPlugin("SchuhBaum.CameraZoom", "CameraZoom", "0.0.1")]
public class MainMod : BaseUnityPlugin {
    // meta data
    public static string mod_id = "CameraZoom";
    public static string author = "SchuhBaum";
    public static string version = "v0.0.1";

    // options
    public static float camera_zoom_multiplier = 1.5f;
    public static bool is_zoom_constant = true;
    public static bool is_zoom_forced = true;

	// variables
    public static bool is_initialized = false;

    protected void Awake() {
        if (is_initialized) return;
        is_initialized = true;
        WobPlugin.Initialise(this, this.Logger);

        WobSettings.Add(new WobSettings.Num<float>("Options", "camera_zoom_multiplier", "Values larger than 1 zoom out the camera. The camera does not zoom out if the room is too small.", 1.5f, bounds: (0.1f, 2.0f)));
        WobSettings.Add(new WobSettings.Boolean("Options", "is_zoom_constant", "When enabled, the zoom does not changed based on the size of the room. The base zoom value stays at 1.", true));
        WobSettings.Add(new WobSettings.Boolean("Options", "is_zoom_forced", "When enabled, the zoom is used in small rooms as well. This creates black borders all around the room.", true));
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
}
