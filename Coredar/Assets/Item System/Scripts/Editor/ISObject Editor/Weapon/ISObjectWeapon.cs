using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem.Editor {
    public partial class ISObjectWeapon {

        protected ISWeaponDatabase db { get; set; }

        const string dbName = @"coredarWeaponDatabase.asset";           // "coredarWeaponDatabase.asset"
        const string dbPath = @"Database";

        public ISObjectWeapon() {

        }

        public string DatabaseFullPath {
            get {
                return @"Assets/" + dbPath + "/" + dbName;
            }
        }

        public void OnEnable() {
            if (db == null)
                db = ISWeaponDatabase.GetDatabase<ISWeaponDatabase>(dbPath, dbName);

            _tempWeapon.OnEnable();
        }
    }
}
