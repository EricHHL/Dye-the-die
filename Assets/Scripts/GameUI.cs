using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public GameObject GameUIPanel;
    public GameObject WinScreen;
    public TMPro.TextMeshProUGUI LevelText;
    public Image star1;
    public Image star2;
    public Image star3;
    public Color inactiveColor;
    public Color activeColor;

    public GameManager gameManager;

    public enum UIState {
        Playing,
        Win,
    }


    public void SetStars(int stars) {
        star1.color = stars >= 1 ? activeColor : inactiveColor;
        star2.color = stars >= 2 ? activeColor : inactiveColor;
        star3.color = stars >= 3 ? activeColor : inactiveColor;
    }

    public void SetState(UIState state) {
        GameUIPanel.SetActive(state == UIState.Playing);
        WinScreen.SetActive(state == UIState.Win);
    }

    public void SetLevel(int level) {
        LevelText.text = "Level " + level;
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

