using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipTile : MonoBehaviour, Tile {

    public int value;
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

    void Start() {
        initializePips();
    }

    void initializePips() {
        int numPips = DOT_POSITIONS[value].Length;
        for (int i = 0; i < numPips; i++) {
            GameObject pip = Instantiate(PipPrefab);
            pip.transform.parent = face.transform;
            Vector2 position = DOT_POSITIONS[value][i];
            pip.transform.localPosition = new Vector3(position.x, 0, position.y);
        }
    }

    public Transform[] getPips() {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in face.transform) {
            children.Add(child);
        }
        return children.ToArray();
    }

    public bool OnPlayerEnter(Player player, DiceFace downwardFace) {
        if (value > 0) {
            // transfer tile's pips to player's face
            Transform[] pips = getPips();

            foreach (Transform pip in pips) {
                pip.parent = downwardFace.transform;
            }

            downwardFace.value = value;
            value = 0;
            return true;
        }
        return false;
    }

    public void OnPlayerEnterReverse(Player player, DiceFace downwardFace) {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in downwardFace.transform) {
            children.Add(child);
        }
        foreach (Transform pip in children) {
            pip.parent = face.transform;
        }

        value = downwardFace.value;
        downwardFace.value = 0;
    }

    public bool CanPlayerEnter(Player player, DiceFace nextFace) {
        if (nextFace.value == 0) return true;

        return value == 0;
    }
}
