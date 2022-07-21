using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Grid grid;
    public Player player;

    public Level[] levels;
    public int currentLevel = 0;
    void Start() {

        player.OnPlayerWin += OnPlayerWin;
        player.OnPlayerLose += OnPlayerLose;

        LoadLevel(0);
    }

    void Update() {

    }

    void OnPlayerWin() {
        if (currentLevel < levels.Length - 1) {
            currentLevel++;
            LoadLevel(currentLevel);
        }
    }

    void OnPlayerLose() {
        LoadLevel(currentLevel);

    }


    void LoadLevel(int levelIndex) {
        currentLevel = levelIndex;
        Level level = levels[levelIndex];
        grid.LoadLevel(level);
        player.Reset();
        player.transform.position = new Vector3(level.initialPosition.x, 0, level.initialPosition.y);
    }
}
