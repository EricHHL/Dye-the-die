using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorItem : MonoBehaviour {
    public TMPro.TextMeshProUGUI levelName;
    public StarsDisplay starsDisplay;

    bool isActive = true;
    Level level;

    public void SetLevel(Level level) {
        levelName.text = level.levelNumber.ToString();
        int stars = Progression.GetLevelStars(level);
        starsDisplay.SetStars(stars);
        this.level = level;
        SetActive(level.levelNumber <= Progression.GetLastLevel() + 1);
    }

    public void OnClick() {
        if (!isActive) {
            return;
        }
        Progression.SetCurrentLevel(level.levelNumber - 1);
        SceneManager.LoadScene("Game");
    }

    public void SetActive(bool active) {
        isActive = active;
        GetComponent<Image>().color = active ? Color.white : Color.gray;
    }
}
