using System.Collections;
using System.Linq; // ElementAt
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Coredar.ItemSystem {
    public class ISQualityDatabase : ScriptableObjectDatabase<ISQuality> {
        
        public int GetIndex(string name) {
            return database.FindIndex(a => a.name == name);
        }
    }
}
