using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform Player;
    //public Transform CameraBox;

    public float RotateSpeed = 5f;
    public float ZoomSpeed = 0.25f;

    Transform Camera;
    void Start() {
        Camera = GetComponent<Transform>();
    }

    void Update() {

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.RotateAround(Player.position, Vector3.down, RotateSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.RotateAround(Player.position, Vector3.up, RotateSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.RotateAround(Player.position, Vector3.right, RotateSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.RotateAround(Player.position, Vector3.left, RotateSpeed);
        }

        var scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue != 0) {
            transform.position *= (1f + -scrollWheelValue * ZoomSpeed);
        }

        Camera.LookAt(Player);
    }
}
