using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Image star1;
    public Image star2;
    public Image star3;

    public Color inactiveColor;
    public Color activeColor;

    public void SetStars(int stars)
    {
        star1.color = stars >= 1 ? activeColor : inactiveColor;
        star2.color = stars >= 2 ? activeColor : inactiveColor;
        star3.color = stars >= 3 ? activeColor : inactiveColor;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
