using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Settings {

    public static bool paused = false;

    /* Values */
    public static float sensitivity = 100f; // 1 - 200
    /* Key Codes */
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode attackKey = KeyCode.Mouse0;
    public static KeyCode pauseKey = KeyCode.Tab;
}