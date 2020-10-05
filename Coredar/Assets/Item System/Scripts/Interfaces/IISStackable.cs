using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coredar.ItemSystem {
    public interface IISStackable {
        int maxStack { get; }
        int Stack(int amount); // default 0

    }
}
