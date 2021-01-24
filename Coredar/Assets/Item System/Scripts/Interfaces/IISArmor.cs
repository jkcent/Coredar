using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISArmor {

        // Stats
        int health { get; set; }
        int defence { get; set; }
        int strength { get; set; }
        int speed { get; set; }
        int dodgeChance { get; set; }
        int critChance { get; set; }
        int critDamage { get; set; }
    }
}
