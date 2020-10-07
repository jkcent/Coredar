﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISWeapon {

        Vector2 damage { get; set; }
        void Attack();
        // range, etc.
    }
}
