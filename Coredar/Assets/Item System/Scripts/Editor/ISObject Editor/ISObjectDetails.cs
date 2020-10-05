using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectEditor {

        ISWeapon _tempWeapon = new ISWeapon();
        bool showNewWeaponDetails = false;

        void ItemDetails() {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (showNewWeaponDetails)
                DisplayNewWeapon();

            GUILayout.EndHorizontal();
            GUILayout.Space(50);
            GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));

            DisplayButtons();

            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        void DisplayNewWeapon() {
            _tempWeapon.OnGUI();
        }

        void DisplayButtons() {
            if (!showNewWeaponDetails) {
                if (GUILayout.Button("Create Weapon")) {
                    _tempWeapon = new ISWeapon();
                    showNewWeaponDetails = true;
                    }
            } else {
                if (GUILayout.Button("Save")) {
                    showNewWeaponDetails = false;
                    #region
                    /*
                    ISQualityDatabase qdb;
                    string FILE_NAME = @"coredarQualityDatabase.asset";
                    string DATABASE_FOLDER_NAME = @"Database";
                    qdb = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_FOLDER_NAME, FILE_NAME);

                    _tempWeapon.Quality = qdb.Get(_tempWeapon.SelectedQualityID);*/
                    #endregion
                    db.Add(_tempWeapon);
                    _tempWeapon = null;
                }

                if (GUILayout.Button("Cancel")) {
                    showNewWeaponDetails = false;
                    _tempWeapon = null;
                }
            }
        }
    }
}
