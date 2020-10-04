using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider sens;
    public Text sensVal;

    private void Start() {
        float sensitivity = Settings.sensitivity;
        sens.value = sensitivity * 100;
        sensVal.text = (sensitivity * 100).ToString();
    }

    public void SetSensitivity() {
        Settings.sensitivity = sens.value / 100;
        sensVal.text = sens.value.ToString();
    }
}
