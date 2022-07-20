using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Tile TilePrefab;
    public int Width = 10;
    public int Height = 10;

    Tile[][] TileGrid;

    void Start() {
        TileGrid = new Tile[Width][];

        for (int i = 0; i < Width; i++) {
            TileGrid[i] = new Tile[Height];
            for (int j = 0; j < Height; j++) {
                Tile tile = Instantiate(TilePrefab);
                tile.value = Random.Range(0, 6);
                tile.transform.parent = transform;
                tile.transform.localPosition = new Vector3(i, 0, j);
                TileGrid[i][j] = tile;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public Tile getTile(Vector3 position) {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);
        if (x < 0 || x >= Width || y < 0 || y >= Height) {
            return null;
        }
        return TileGrid[x][y];
    }
}
