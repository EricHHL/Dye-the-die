using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Grid grid;
    public Player player;

    public Level[] levels;
    public int currentLevel = 0;

    public GameObject WinScreen;
    public GameCamera cameraController;

    Stack<PlayerMove> moves = new Stack<PlayerMove>();

    struct PlayerMove {
        public Vector3 position;
        public bool triggeredAction;
    }

    void Start() {

        player.OnPlayerWin += OnPlayerWin;
        player.OnPlayerLose += OnPlayerLose;
        player.OnPlayerMove += OnPlayerMove;

        LoadLevel(0);
    }

    void Update() {
        if (Input.GetButtonDown("Restart")) {
            Restart();
        }
        if (Input.GetButton("Undo")) {
            Undo();
        }
    }

    void OnPlayerWin() {
        WinScreen.SetActive(true);
        Vector3 target = cameraController.diceTarget.TransformPoint(Vector3.zero);
        player.VictoryAnim(target);
    }

    void OnPlayerLose() {
        Restart();
    }

    void OnPlayerMove(Vector3 newPosition, bool isUndo) {
        if(isUndo) return;
        Tile tile = grid.GetTile(player.transform.position);
        DiceFace downwardFace = player.GetFaceFacingDirection(Vector3.down);

        bool triggeredAction = tile.OnPlayerEnter(player, downwardFace);
        moves.Push(new PlayerMove { position = newPosition, triggeredAction = triggeredAction });
    }

    void Undo() {
        if (moves.Count > 1 && player.isMoving == false) {
            PlayerMove currentMove = moves.Pop();

            // roll player back to the previous position
            PlayerMove lastMove = moves.Peek();
            bool success = player.RollToDirection(lastMove.position - player.transform.position, true);
            // for some reason the move can't be udone, so rollback
            if(!success) {
                moves.Push(currentMove);
                return;
            }
            // if the move triggered an action in the tile, undo it
            if (currentMove.triggeredAction) {
                Tile tile = grid.GetTile(currentMove.position);
                DiceFace downwardFace = player.GetFaceFacingDirection(Vector3.down);
                tile.OnPlayerEnterReverse(player, downwardFace);
            }

        }
    }

    void Restart() {
        LoadLevel(currentLevel);
        moves.Clear();
    }

    void LoadLevel(int levelIndex) {
        currentLevel = levelIndex;
        Level level = levels[levelIndex];
        grid.LoadLevel(level, player);

        WinScreen.SetActive(false);
    }

    public void OnNextLevelButtonPressed() {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public void OnHomeButtonPressed() {
        // Application.LoadLevel("MainMenu");
    }
}
