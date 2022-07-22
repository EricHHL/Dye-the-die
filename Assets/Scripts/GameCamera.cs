using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCamera : MonoBehaviour {
    public Grid grid;
    public Transform diceTarget;


    void Awake() {
        grid.OnLevelLoaded += OnLevelLoaded;
    }

    // Update is called once per frame
    void Update() {
        float rotation = Input.GetAxis("Rotate");
        if (rotation != 0) {
            transform.Rotate(0, rotation * Time.deltaTime * 100, 0);
        }
    }

    void OnLevelLoaded(Level level) {
        transform.DOMove(new Vector3(level.width - 1, -1f, level.height - 1) / 2, 0.5f).SetEase(Ease.InOutQuad);
    }
}
