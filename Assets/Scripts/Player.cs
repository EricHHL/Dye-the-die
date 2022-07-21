using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    private bool isMoving = false;
    public Grid grid;

    public AnimationCurve tryRollAnimCurve;

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

    public void Reset() {
        isMoving = false;
        foreach (DiceFace face in faces) {
            if (face.transform != null) {
                Destroy(face.transform.gameObject);
            }
        }
        InitializeFaces();
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

    bool RollToDirection(Vector3 direction) {
        if (isMoving) return false;
        isMoving = true;

        if (!CanRollTo(direction)) {
            RollAnim(direction, 12, 0.4f).SetEase(tryRollAnimCurve).OnComplete(() => {
                isMoving = false;
            });
            return false;
        }

        RollAnim(direction, 90, 0.4f).SetEase(Ease.InQuad).OnComplete(() => {
            isMoving = false;
            OnMovementEnd();
        });
        return true;
    }

    Tween RollAnim(Vector3 direction, float degrees, float duration) {
        var anchor = transform.position + (direction + Vector3.down) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, direction);

        float rot = 0f;
        return DOTween.To(() => rot, newRot => {
            transform.RotateAround(anchor, axis, newRot - rot);
            rot = newRot;
        }, degrees, duration);
    }

    void OnMovementEnd() {
        Tile tile = grid.GetTile(transform.position);

        if (tile.value > 0) {
            DiceFace downwardFace = GetFaceFacingDirection(Vector3.down);

            // transfer tile's pips to player's face
            Transform[] pips = tile.getPips();
            foreach (Transform pip in pips) {
                pip.parent = downwardFace.transform;
            }

            downwardFace.value = tile.value;
            tile.value = 0;
        }
        DiceState state = GetDiceState();
        if (state == DiceState.Valid) {
            OnPlayerWin();
        } else if (state == DiceState.Invalid) {
            OnPlayerLose();
        }
    }

    DiceFace GetFaceFacingDirection(Vector3 direction) {
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        foreach (DiceFace face in faces) {
            if (face.direction == localDirection) {
                return face;
            }
        }
        return null;
    }

    bool CanRollTo(Vector3 direction) {
        Tile nextTile = grid.GetTile(transform.position + direction);
        if (nextTile == null) return false;

        if (nextTile.value == 0) return true;

        DiceFace nextFaceDown = GetFaceFacingDirection(direction);
        return nextFaceDown.value == 0;
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
