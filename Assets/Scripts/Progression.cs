using UnityEngine;

public class Progression {

    public static void setLevelComplete(int level, int stars) {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
        if(currentLevel < level) {
            PlayerPrefs.SetInt("currentLevel", level);
        }
        int currentStars = PlayerPrefs.GetInt("stars_" + level, 0);
        if(currentStars < stars) {
            PlayerPrefs.SetInt("stars_" + level, stars);
        }
    }
}
