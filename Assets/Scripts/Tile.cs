using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int value;
    Vector2[][] DOT_POSITIONS = new Vector2[][]
    {
        new Vector2[]{},
        new Vector2[]
        {
            new Vector2(0, 0),
        },
        new Vector2[]
        {
            new Vector2(-0.25f, -0.25f), new Vector2(0.25f, 0.25f)
        },
        new Vector2[]
        {
            new Vector2(-0.25f, -0.25f), new Vector2(0, 0), new Vector2(0.25f, 0.25f)
        },
        new Vector2[]
        {
            new Vector2(-0.25f, -0.25f), new Vector2(-0.25f, 0.25f), new Vector2(0.25f, 0.25f), new Vector2(0.25f, -0.25f), new Vector2(0,0)
        },
        new Vector2[]{
            new Vector2(-0.25f, -0.25f), new Vector2(-0.25f, 0.25f), new Vector2(0.25f, 0.25f), new Vector2(0.25f, -0.25f), new Vector2(0, 0.25f), new Vector2(0, -0.25f)
        }
    };
    public GameObject PipPrefab;
    public GameObject pipParent;

    void Start() {
        initializePips();
    }

    void initializePips() {
        int numPips = DOT_POSITIONS[value].Length;
        for (int i = 0; i < numPips; i++) {
            GameObject pip = Instantiate(PipPrefab);
            pip.transform.parent = pipParent.transform;
            Vector2 position = DOT_POSITIONS[value][i];
            pip.transform.localPosition = new Vector3(position.x, 0, position.y);
        }
    }
}
