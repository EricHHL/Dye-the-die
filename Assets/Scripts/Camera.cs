using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public Grid grid;

    void Start() {
        grid.OnLevelLoaded += OnLevelLoaded;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnLevelLoaded(Level level) {
        transform.position = new Vector3(level.width, 0, level.height) / 2;
    }
}
