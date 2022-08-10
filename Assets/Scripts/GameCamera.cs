using UnityEngine;
using DG.Tweening;

public class GameCamera : MonoBehaviour {
    [SerializeField]
    Grid grid;
    [SerializeField]

    public Transform diceTarget;

    Vector2 previousMousePosition;
    bool isRotating;
    void Awake() {
        grid.OnLevelLoaded += OnLevelLoaded;
    }

    void Update() {
        float rotation = Input.GetAxis("Rotate");
        if (rotation != 0) {
            transform.Rotate(0, rotation * Time.deltaTime * 200, 0);
        }

        if (Input.GetMouseButtonDown(0)) {
            isRotating = true;
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) {
            isRotating = false;
        }

        if (isRotating) {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - previousMousePosition;
            transform.Rotate(0, delta.x * Time.deltaTime * 5, 0);
            previousMousePosition = currentMousePosition;
        }
    }

    void OnLevelLoaded(Level level) {
        transform.DOMove(new Vector3(level.width - 1, -1f, level.height - 1) / 2, 0.5f).SetEase(Ease.InOutQuad);
    }
}
