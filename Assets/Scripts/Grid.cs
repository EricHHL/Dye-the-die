using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Tile TilePrefab;
    void Start() {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                Tile tile = Instantiate(TilePrefab);
                tile.value = Random.Range(0, 6);
                tile.transform.parent = transform;
                tile.transform.localPosition = new Vector3(i, 0, j);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
