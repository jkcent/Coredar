﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {
    #region Static Booleans
    public static bool paused = false;
    #endregion
    #region Values
    public static float sensitivity = 0.65f; // 0.01 - 2 (* 100 in the menus)
    public static int zoomSpeed = 1; // 1 - 100
    #endregion
    #region KeyCodes
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode attackKey = KeyCode.Mouse0;
    public static KeyCode pauseKey = KeyCode.Tab;
    public static KeyCode inventoryKey = KeyCode.I;
    public static KeyCode accessKey = KeyCode.E;
    #endregion
}