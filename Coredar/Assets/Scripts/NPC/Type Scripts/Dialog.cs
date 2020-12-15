using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {

    public TMP_Text text;
    float delay = 0.1f;

    List<string> queuedDialog = new List<string>();
    NPC npc;

    IEnumerator WriteDialog() {
        for (int i = 0; i < queuedDialog.Count; i++) {
            text.text = string.Empty;
            float tempDelay = delay;
            foreach (char letter in queuedDialog[i]) {
                if (Input.GetKey(Settings.accessKey)) {
                    tempDelay = delay / 2;
                } else {
                    tempDelay = delay;
                }
                if (letter == '|')
                    text.text += "\n";
                else
                    text.text += letter;
                yield return new WaitForSeconds(tempDelay);
            }
            bool proceed = false;
            while (!proceed) {
                if (Input.GetKeyDown(Settings.accessKey)) {
                    proceed = true;
                }
                yield return new WaitForSeconds(0f);
            }
        }

        npc.StopDialog();
    }

    public void AccessCoroutine(List<string> dialog, NPC npc) {
        queuedDialog = dialog;
        this.npc = npc;
        StartCoroutine(WriteDialog());
    }
}
