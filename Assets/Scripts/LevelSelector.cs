using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelSelectorItemPrefab;
    
    public Level[] levels;

    void Start()
    {
        foreach (Level level in levels)
        {
            GameObject levelSelectorItem = Instantiate(levelSelectorItemPrefab);
            levelSelectorItem.transform.SetParent(transform, false);
            levelSelectorItem.GetComponent<LevelSelectorItem>().SetLevel(level);
        }
    }
}
