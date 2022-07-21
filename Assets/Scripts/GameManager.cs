using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public Player player;

    public Level level;
    void Start()
    {
        grid.LoadLevel(level);
        player.transform.position = new Vector3(level.initialPosition.x,0,level.initialPosition.y);   

    }

    void Update() {
        
    }
}
