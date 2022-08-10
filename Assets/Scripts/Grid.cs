using UnityEngine;
using DG.Tweening;

// Responsible for managing the loaded level, handling the Tile instances
public class Grid : MonoBehaviour {

    [SerializeField]
    GameObject PipTilePrefab;
    [SerializeField]
    GameObject TrashTilePrefab;

    ITile[,] TileGrid;

    public delegate void LevelLoadEvent(Level level);
    public event LevelLoadEvent OnLevelLoaded;
    public event System.Action OnLevelDeloaded;

    bool isLevelLoading = false;

    public ITile GetTile(Vector3 position) {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);
        if (x < 0 || x >= TileGrid.GetLength(0) || y < 0 || y >= TileGrid.GetLength(1)) {
            return null;
        }
        return TileGrid[x, y];
    }

    public void LoadLevel(Level level, Player player) {
        if (isLevelLoading) {
            return;
        }
        DeloadLevel();
        isLevelLoading = true;

        TileGrid = new ITile[level.width, level.height];

        for (int y = 0; y < level.height; y++) {
            for (int x = 0; x < level.width; x++) {
                int pos = y * level.width + x;
                if (level.tiles[pos] != -1)
                    InstantiateTile(x, y, level.tiles[pos]);
            }
        }
        player.Reset(new Vector3(level.initialPosition.x, 10, level.initialPosition.y));
        isLevelLoading = false;
        if (OnLevelLoaded != null)
            OnLevelLoaded(level);
    }

    void DeloadLevel() {
        foreach (Transform child in transform) {
            child.DOMoveY(child.position.y - 15, 0.3f).SetEase(Ease.InCubic).OnComplete(() => {
                Destroy(child.gameObject);
            });
        }
        if (OnLevelDeloaded != null)
            OnLevelDeloaded();
    }

    public void InstantiateTile(int x, int y, int type) {
        GameObject tileGO;
        ITile tile;
        switch (type) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                tileGO = Instantiate(PipTilePrefab);
                tile = tileGO.GetComponent<PipTile>();
                ((PipTile)tile).value = type;
                break;
            case 7:
                tileGO = Instantiate(TrashTilePrefab);
                tile = tileGO.GetComponent<TrashTile>();
                break;
            default:
                return;
        }

        TileGrid[x, y] = tile;
        tileGO.transform.parent = transform;
        tileGO.transform.localPosition = new Vector3(x, 10, y);
        tileGO.transform.DOMoveY(0, 0.4f).SetEase(Ease.OutCubic).SetDelay((float)(x + y) / 20f).SetDelay((float)(x + y) / 20f);
    }

}
