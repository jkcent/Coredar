using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {

    public static bool paused = false;

    /* Values */
    public static float sensitivity = 0.65f; // 0.01 - 2 (* 100 in the menus)
    /* Key Codes */
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode attackKey = KeyCode.Mouse0;
    public static KeyCode pauseKey = KeyCode.Tab;
}