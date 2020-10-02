using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider sens;
    public Text sensVal;

    private void Start() {
        float sensitivity = Mathf.FloorToInt(Settings.sensitivity);
        sens.value = sensitivity;
        sensVal.text = sensitivity.ToString();
    }

    public void SetSensitivity() {
        Settings.sensitivity = sens.value;
        sensVal.text = sens.value.ToString();
    }
}
