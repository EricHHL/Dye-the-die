using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    public bool isMoving = false;
    public Grid grid;

    public AnimationCurve tryRollAnimCurve;

    public delegate void PlayerMoveEvent(Vector3 newPosition, bool isUndo);
    public event PlayerMoveEvent OnPlayerMove;

    public event System.Action OnPlayerWin;
    public event System.Action OnPlayerLose;

    DiceFace[] faces = new DiceFace[]{
        new DiceFace{direction = Vector3.forward, name = "forward"},
        new DiceFace{direction = Vector3.back, name = "back"},
        new DiceFace{direction = Vector3.right, name = "right"},
        new DiceFace{direction = Vector3.left, name = "left"},
        new DiceFace{direction = Vector3.up, name = "up"},
        new DiceFace{direction = Vector3.down, name = "down"}
    };

    List<Tween> currentTweens = new List<Tween>();

    enum DiceState { Incomplete, Valid, Invalid }

    void Start() {
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            RollToDirection(Vector3.right);
        }
        if (Input.GetKey(KeyCode.A)) {
            RollToDirection(Vector3.left);
        }
        if (Input.GetKey(KeyCode.W)) {
            RollToDirection(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S)) {
            RollToDirection(Vector3.back);
        }
    }

    public void Reset(Vector3 position) {
        isMoving = true;
        transform.localScale = Vector3.one;
        foreach (DiceFace face in faces) {
            if (face.transform != null) {
                Destroy(face.transform.gameObject);
            }
        }
        InitializeFaces();
        transform.DOComplete();
        foreach (Tween t in currentTweens) {
            t.Rewind();
            t.Kill();
        }
        currentTweens.Clear();


        transform.position = position + Vector3.up * 10;

        transform.DOMoveY(0, 0.6f).SetEase(Ease.OutBounce).SetDelay(0.3f).OnComplete(() => {
            isMoving = false;
            OnMovementEnd();
        });
    }

    void InitializeFaces() {
        foreach (DiceFace face in faces) {
            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = face.direction * 0.5f;
            go.name = "Face " + face.name;
            face.transform = go.transform;
            face.value = 0;
        }
    }

    public bool RollToDirection(Vector3 direction, bool isUndo = false) {
        if (isMoving) return false;
        isMoving = true;

        if (!CanRollTo(direction)) {
            RollAnim(direction, 12, 0.4f).SetEase(tryRollAnimCurve).OnComplete(() => {
                isMoving = false;
            });
            return false;
        }

        RollAnim(direction, 90, isUndo ? 0.1f : 0.4f).SetEase(Ease.InQuad).OnComplete(() => {
            isMoving = false;
            OnMovementEnd(isUndo);
        });
        return true;
    }

    Tween RollAnim(Vector3 direction, float degrees, float duration) {
        var anchor = transform.position + (direction + Vector3.down) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, direction);

        float rot = 0f;
        Tween tween = DOTween.To(() => rot, newRot => {
            transform.RotateAround(anchor, axis, newRot - rot);
            rot = newRot;
        }, degrees, duration);
        currentTweens.Add(tween);
        return tween;
    }

    public void VictoryAnim(Vector3 target) {
        currentTweens.Add(transform.DOMove(target, 0.5f).SetEase(Ease.InOutQuad));
        currentTweens.Add(transform.DOScale(2, 0.5f).SetEase(Ease.InOutQuad));
        currentTweens.Add(transform.DORotate(new Vector3(360, 1080, 360), 9f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart));
    }

    void OnMovementEnd(bool isUndo = false) {
        if (OnPlayerMove != null) {
            OnPlayerMove(transform.position, isUndo);
        }

        DiceState state = GetDiceState();
        if (state == DiceState.Valid) {
            OnPlayerWin();
        } else if (state == DiceState.Invalid) {
            OnPlayerLose();
        }
    }

    public DiceFace GetFaceFacingDirection(Vector3 direction) {
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        float closest = float.MaxValue;
        DiceFace closestFace = null;

        foreach (DiceFace face in faces) {
            float distance = Vector3.Distance(localDirection, face.direction);
            if (distance < closest) {
                closest = distance;
                closestFace = face;
            }
        }
        return closestFace;
    }

    bool CanRollTo(Vector3 direction) {
        Tile nextTile = grid.GetTile(transform.position + direction);
        if (nextTile == null) return false;

        DiceFace nextFaceDown = GetFaceFacingDirection(direction);
        if (nextFaceDown == null) return false;
        return nextTile.CanPlayerEnter(this, nextFaceDown);
    }

    DiceState GetDiceState() {
        foreach (DiceFace face in faces) {
            if (face.value == 0) {
                return DiceState.Incomplete;
            }
        }
        if (faces[0].value + faces[1].value == 7 &&
            faces[2].value + faces[3].value == 7 &&
            faces[4].value + faces[5].value == 7)
            return DiceState.Valid;
        return DiceState.Invalid;
    }

}
