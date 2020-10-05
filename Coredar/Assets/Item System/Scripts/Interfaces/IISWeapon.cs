using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISWeapon {

        int minDamage { get; set; }
        int Attack();
        // range, etc.
    }
}
