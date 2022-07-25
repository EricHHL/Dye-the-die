using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public GameObject GameUIPanel;
    public GameObject WinScreen;
    public TMPro.TextMeshProUGUI LevelText;

    public GameManager gameManager;
    public StarsDisplay starsDisplay;

    public enum UIState {
        Playing,
        Win,
    }

    public void SetState(UIState state) {
        GameUIPanel.SetActive(state == UIState.Playing);
        WinScreen.SetActive(state == UIState.Win);
    }

    public void SetStars(int stars) {
        starsDisplay.SetStars(stars);
    }

    public void SetLevel(int level) {
        LevelText.text = "LEVEL " + level;
    }

    public void OnButtonMenuPressed() {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonRestartPressed() {
        gameManager.Restart();
    }

    public void OnButtonUndoPressed() {
        gameManager.Undo();
    }

    public void OnNextLevelButtonPressed() {
        gameManager.NextLevel();
    }
}

