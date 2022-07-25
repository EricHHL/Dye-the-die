using UnityEngine;

public class Progression {

    static int currentLevel = 0;

    public static void SetLevelComplete(Level level, int stars) {
        int lastLevel = PlayerPrefs.GetInt("lastLevel", 0);
        if (lastLevel < level.levelNumber) {
            PlayerPrefs.SetInt("lastLevel", level.levelNumber);
        }
        int currentStars = GetLevelStars(level);
        if (currentStars < stars) {
            PlayerPrefs.SetInt("stars_" + level.levelNumber, stars);
        }
        PlayerPrefs.Save();
    }

    public static int GetLevelStars(Level level) {
        int stars = PlayerPrefs.GetInt("stars_" + level.levelNumber, -1);
        return stars;
    }

    public static int GetLastLevel() {
        return PlayerPrefs.GetInt("lastLevel", 0);
    }

    public static int GetCurrentLevel() {
        return currentLevel;
    }

    public static void SetCurrentLevel(int level) {
        currentLevel = level;
    }
}
