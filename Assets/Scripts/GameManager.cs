using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Grid grid;
    public Player player;

    public Level[] levels;
    public int currentLevel = 0;

    public GameObject WinScreen;

    void Start() {

        player.OnPlayerWin += OnPlayerWin;
        player.OnPlayerLose += OnPlayerLose;

        LoadLevel(0);
    }

    void Update() {
        if (Input.GetButtonDown("Restart")) {
            Restart();
        }
    }

    void OnPlayerWin() {
        WinScreen.SetActive(true);
    }

    void OnPlayerLose() {
        Restart();
    }

    void Restart(){
        LoadLevel(currentLevel);
    }

    void LoadLevel(int levelIndex) {
        currentLevel = levelIndex;
        Level level = levels[levelIndex];
        grid.LoadLevel(level);
        player.Reset();
        player.transform.position = new Vector3(level.initialPosition.x, 0, level.initialPosition.y);

        WinScreen.SetActive(false);
    }

    public void OnNextLevelButtonPressed() {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public void OnHomeButtonPressed() {
        // Application.LoadLevel("MainMenu");
    }
}
