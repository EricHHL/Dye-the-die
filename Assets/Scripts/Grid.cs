using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Tile TilePrefab;

    Tile[,] TileGrid;

    public delegate void LevelLoadEvent(Level level);
    public event LevelLoadEvent OnLevelLoaded;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public Tile GetTile(Vector3 position) {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);
        if (x < 0 || x >= TileGrid.GetLength(0) || y < 0 || y >= TileGrid.GetLength(1)) {
            return null;
        }
        return TileGrid[x, y];
    }

    public void LoadLevel(Level level) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        TileGrid = new Tile[level.width, level.height];

        for (int y = 0; y < level.height; y++) {
            for (int x = 0; x < level.width; x++) {
                int pos = y * level.width + x;
                if (level.tiles[pos] != -1)
                    InstantiateTile(x, y, level.tiles[pos]);
            }
        }
        if (OnLevelLoaded != null)
            OnLevelLoaded(level);
    }

    public void InstantiateTile(int x, int y, int type) {
        Tile tile = Instantiate(TilePrefab);
        tile.value = type;
        tile.transform.parent = transform;
        tile.transform.localPosition = new Vector3(x, 0, y);
        TileGrid[x, y] = tile;
    }

}
