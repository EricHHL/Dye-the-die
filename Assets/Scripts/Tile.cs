using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Tile {
    

    bool OnPlayerEnter(Player player, DiceFace downwardFace);
    void OnPlayerEnterReverse(Player player, DiceFace downwardFace);
    bool CanPlayerEnter(Player player, DiceFace nextFace);
}
