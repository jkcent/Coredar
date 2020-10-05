using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    [System.Serializable]
    public class ISQuality : IISQuality {

        [SerializeField]string _name;
        [SerializeField]Color _color;

        public ISQuality() {
            _name = "";
            _color = new Color();
        }
        public ISQuality(string name, Color color) {
            _name = name;
            _color = color;
        }

        public string name {
            get { return _name; }
            set { _name = value; }
        }
        public Color color {
            get { return _color; }
            set { _color = value; }
        }
    }
}