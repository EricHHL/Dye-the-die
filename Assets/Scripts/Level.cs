using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1), System.Serializable]
public class Level : ScriptableObject {
    [SerializeField, HideInInspector]
    public int[] tiles;
    [SerializeField]
    public int width = 6;
    [SerializeField]
    public int height = 6;

    public Vector2 initialPosition;
}