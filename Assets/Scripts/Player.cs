using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    private bool isMoving = false;
    public Grid grid;

    public AnimationCurve tryRollAnimCurve;

    DiceFace[] faces = new DiceFace[]{
        new DiceFace{direction = Vector3.forward, name = "forward"},
        new DiceFace{direction = Vector3.right, name = "right"},
        new DiceFace{direction = Vector3.back, name = "back"},
        new DiceFace{direction = Vector3.left, name = "left"},
        new DiceFace{direction = Vector3.up, name = "up"},
        new DiceFace{direction = Vector3.down, name = "down"}
    };


    void Start() {
        initializeFaces();
    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            roll(Vector3.right);
        }
        if (Input.GetKey(KeyCode.A)) {
            roll(Vector3.left);
        }
        if (Input.GetKey(KeyCode.W)) {
            roll(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S)) {
            roll(Vector3.back);
        }
    }

    bool roll(Vector3 direction) {
        if (isMoving) return false;

        isMoving = true;
        var anchor = transform.position + (direction + Vector3.down) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, direction);
        float rot = 0f;

        if (!canRollTo(direction)) {
            DOTween.To(() => rot, newRot => {
                transform.RotateAround(anchor, axis, newRot - rot);
                rot = newRot;
            }, 12, 0.3f).SetEase(tryRollAnimCurve).OnComplete(() => {
                isMoving = false;
            });
            return false;
        }

        DOTween.To(() => rot, newRot => {
            transform.RotateAround(anchor, axis, newRot - rot);
            rot = newRot;
        }, 90, 0.4f).SetEase(Ease.InQuad).OnComplete(() => {
            isMoving = false;
            onMovementEnd();
        });
        return true;
    }

    void onMovementEnd() {
        Tile tile = grid.getTile(transform.position);

        DiceFace downwardFace = getFaceFacingDirection(Vector3.down);

        // transfer tile's pips to player's face
        Transform[] pips = tile.getPips();
        foreach (Transform pip in pips) {
            pip.parent = downwardFace.transform;
        }

        downwardFace.value = tile.value;
    }

    void initializeFaces() {
        foreach (DiceFace face in faces) {
            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = face.direction * 0.5f;
            go.name = "Face " + face.name;
            face.transform = go.transform;
            face.value = 0;
        }
    }

    DiceFace getFaceFacingDirection(Vector3 direction) {
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        foreach (DiceFace face in faces) {
            if (face.direction == localDirection) {
                return face;
            }
        }
        return faces[0];
    }

    bool canRollTo(Vector3 direction) {
        Tile nextTile = grid.getTile(transform.position + direction);
        if (nextTile == null) return false;

        DiceFace nextFaceDown = getFaceFacingDirection(direction);

        return nextFaceDown.value == 0;
    }

}
