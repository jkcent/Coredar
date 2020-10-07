using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectWeapon {

        public void ItemDetails() {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (showDetails) {
                DisplayWeapon();
            }

            GUILayout.EndVertical();
            GUILayout.Space(50);
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            DisplayButtons();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        void DisplayWeapon() {
            _tempWeapon.OnGUI();
        }

        void DisplayButtons() {
            if (!createNewWeapon) {
                if (GUILayout.Button("Create Weapon")) {
                    _tempWeapon = new ISWeapon();
                    createNewWeapon = true;
                    showDetails = true;
                }
            } else {
                GUI.SetNextControlName("SaveButton");

                if (GUILayout.Button("Save")) {
                    if (_selectedIndex == -1) {
                        db.Add(_tempWeapon);
                    } else {
                        db.Replace(_selectedIndex, _tempWeapon);
                    }

                    createNewWeapon = false;
                    _tempWeapon = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }

                if (_selectedIndex != -1) {
                    if (GUILayout.Button("Delete")) {
                        if (EditorUtility.DisplayDialog("Delete Weapon", "Are you sure that you want to delete \"" + db.Get(_selectedIndex).name + "\"?", "Yes", "No")) {
                            db.RemoveAt(_selectedIndex);
                            createNewWeapon = false;
                            _tempWeapon = null;
                            showDetails = false;
                            _selectedIndex = -1;
                            GUI.FocusControl("SaveButton");
                        }
                    }
                }

                if (GUILayout.Button("Cancel")) {
                    createNewWeapon = false;
                    _tempWeapon = null;
                    showDetails = false;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }
            }
        }
    }
}
