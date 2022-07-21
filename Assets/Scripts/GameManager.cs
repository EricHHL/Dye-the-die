using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Grid grid;
    public Player player;

    public Level[] levels;
    public int currentLevel = 0;

    public GameObject WinScreen;
    public Camera cameraController;

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
        Vector3 target = cameraController.diceTarget.TransformPoint(Vector3.zero);
        player.VictoryAnim(target);
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
        grid.LoadLevel(level, player);

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
