using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISWeapon {

        Vector2 damage { get; set; }
        int attackRange { get; set; }

        // Stats
        int health { get; set; }
        int defence { get; set; }
        int strength { get; set; }
        int speed { get; set; }
        int dodgeChance { get; set; }
        int critChance { get; set; }
        int critDamage { get; set; }
        
        void Attack();
    }
}
