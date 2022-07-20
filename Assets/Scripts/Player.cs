using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    private bool isMoving = false;
    public Grid grid;

    void Start() {

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
        var tile = grid.getTile(transform.position);

        Transform[] pips = tile.getPips();
        foreach (Transform pip in pips) {
            pip.parent = transform;
        }   
    }

}
