using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceMap : MonoBehaviour {
    [SerializeField]
    Player player;
    [SerializeField]
    Grid grid;
    [SerializeField]
    Sprite[] diceFaceSprites;
    [SerializeField]
    GameObject downwardIndicator;

    Dictionary<string, Image> diceFaces = new Dictionary<string, Image>();

    void Start() {
        foreach (Transform child in transform) {
            diceFaces.Add(child.name, child.GetComponent<Image>());
        }
        player.OnPlayerChanged += UpdateMapState;
        grid.OnLevelDeloaded += Clear;
    }

    void UpdateMapState() {
        foreach (DiceFace face in player.faces) {
            diceFaces[face.name].sprite = diceFaceSprites[(int)face.value];
        }
        DiceFace downwardFace = player.GetFaceFacingDirection(Vector3.down);

        downwardIndicator.transform.position = diceFaces[downwardFace.name].transform.position;
    }

    void Clear() {
        foreach (KeyValuePair<string, Image> face in diceFaces) {
            face.Value.sprite = diceFaceSprites[0];
        }
        downwardIndicator.transform.position = diceFaces["down"].transform.position;
    }

}
