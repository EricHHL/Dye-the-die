using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    void Start() {

    }

    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            roll(new Vector3(1, 0, 0));
        }
    }

    void roll(Vector3 direction) {
        //use tween to roll to direction
        var anchor = transform.position + direction;

    }
}
