using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipTile : MonoBehaviour, ITile {

    public int value;
    int originalValue;
    Vector2[][] DOT_POSITIONS = new Vector2[][]
    {
        new Vector2[] {},
        new Vector2[] {
            new Vector2(0, 0),
        },
        new Vector2[] {
            new Vector2(-0.25f, -0.25f), new Vector2(0.25f, 0.25f)
        },
        new Vector2[] {
            new Vector2(-0.25f, -0.25f), new Vector2(0, 0), new Vector2(0.25f, 0.25f)
        },
        new Vector2[] {
            new Vector2(-0.25f, -0.25f), new Vector2(-0.25f, 0.25f), new Vector2(0.25f, 0.25f), new Vector2(0.25f, -0.25f)
        },
        new Vector2[] {
            new Vector2(-0.25f, -0.25f), new Vector2(-0.25f, 0.25f), new Vector2(0.25f, 0.25f), new Vector2(0.25f, -0.25f), new Vector2(0,0)
        },
        new Vector2[] {
            new Vector2(-0.25f, -0.25f), new Vector2(-0.25f, 0.25f), new Vector2(0.25f, 0.25f), new Vector2(0.25f, -0.25f), new Vector2(0, 0.25f), new Vector2(0, -0.25f)
        }
    };
    public GameObject PipPrefab;
    public Transform face;

    List<GameObject> myPips;

    void Start() {
        initializePips();
    }

    void initializePips() {
        myPips = new List<GameObject>();
        int numPips = DOT_POSITIONS[value].Length;
        for (int i = 0; i < numPips; i++) {
            GameObject pip = Instantiate(PipPrefab);
            pip.transform.parent = face.transform;
            Vector2 position = DOT_POSITIONS[value][i];
            pip.transform.localPosition = new Vector3(position.x, 0, position.y);
            myPips.Add(pip);
        }
    }

    public bool OnPlayerEnter(Player player, DiceFace downwardFace) {
        if (value == 0) return false;

        // transfer tile's pips to player's face
        foreach (GameObject pip in myPips) {
            pip.transform.parent = downwardFace.transform;
        }
        downwardFace.value += value;
        originalValue = value;
        value = 0;
        return true;

    }

    public void OnPlayerEnterReverse(Player player, DiceFace downwardFace) {
        foreach (GameObject pip in myPips) {
            pip.transform.parent = face.transform;
        }

        value = originalValue;
        downwardFace.value -= originalValue;
    }

    public bool CanPlayerEnter(Player player, DiceFace nextFace) {
        if (value == 0) return true;
        switch (nextFace.value) {
            case 0:
                return true;
            case 1:
                return value == 2 || value == 4;
            case 2:
                return value == 1;
            case 4:
                return value == 1;
            default:
                return false;
        }
    }
}
