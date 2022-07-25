using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour {
    public Image star1;
    public Image star2;
    public Image star3;
    public Color inactiveColor = new Color(0.44f, 0.44f, 0.44f, 1f);
    public Color activeColor = new Color(1, 0.846f, 0, 1f);

    public void SetStars(int stars) {
        star1.color = stars >= 1 ? activeColor : inactiveColor;
        star2.color = stars >= 2 ? activeColor : inactiveColor;
        star3.color = stars >= 3 ? activeColor : inactiveColor;
    }
}
