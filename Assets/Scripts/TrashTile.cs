using System.Collections.Generic;
using UnityEngine;

public class TrashTile : MonoBehaviour, ITile {

    public GameObject trashIcon;

    bool isActive = true;
    int removedValue = 0;
    List<Transform> RemovedPips = new List<Transform>();

    void SetActive(bool active) {
        isActive = active;
        trashIcon.SetActive(active);
    }

    public bool OnPlayerEnter(Player player, DiceFace downwardFace) {
        if (!isActive || downwardFace.value == 0) return false;

        removedValue = downwardFace.value;
        RemovedPips = new List<Transform>();
        foreach (Transform pip in downwardFace.transform) {
            RemovedPips.Add(pip);
        }
        foreach (Transform pip in RemovedPips) {
            pip.parent = transform;
            pip.gameObject.SetActive(false);
        }
        downwardFace.value = 0;
        SetActive(false);
        return true;
    }

    public void OnPlayerEnterReverse(Player player, DiceFace downwardFace) {
        foreach (Transform pip in RemovedPips) {
            pip.parent = downwardFace.transform;
            pip.gameObject.SetActive(true);
        }
        downwardFace.value = removedValue;
        SetActive(true);
    }

    public bool CanPlayerEnter(Player player, DiceFace nextFace) {
        return true;
    }
}
