using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBehaviour : MonoBehaviour {
    //unity lab 3 and 4 scripts

    public float XSpeed;
    public float YSpeed;
    public float ZSpeed;
    public float Multiplier = 0.75f;
    public float TooFar = 5;

    static int BallCount = 0;
    public int BallNumber;

    void Start() {

        BallCount++;
        BallNumber = BallCount;
        //transform.position = new Vector3(3 - Random.value * 6, 3 - Random.value * 6, 3 - Random.value * 6);
        ResetBall();
    }

    void Update() {
        transform.Translate(Time.deltaTime * XSpeed, Time.deltaTime * YSpeed, Time.deltaTime * ZSpeed);

        XSpeed += Multiplier - Random.value * Multiplier * 2;
        YSpeed += Multiplier - Random.value * Multiplier * 2;
        ZSpeed += Multiplier - Random.value * Multiplier * 2;

        if ((Mathf.Abs(transform.position.x) > TooFar) || (Mathf.Abs(transform.position.y) > TooFar) || (Mathf.Abs(transform.position.z) > TooFar)) {
            ResetBall();
        }
    }

    private void OnMouseDown() {
        GameController controller = Camera.main.GetComponent<GameController>();
        if (!controller.GameOver) {
            controller.ClickedOnBall();
            Destroy(gameObject);
        }
    }

    private void ResetBall() {
        XSpeed += Multiplier - Random.value * Multiplier * 2;
        YSpeed += Multiplier - Random.value * Multiplier * 2;
        ZSpeed += Multiplier - Random.value * Multiplier * 2;

        transform.position = new Vector3(3 - Random.value * 6, 3 - Random.value * 6, 3 - Random.value * 6);
    }
}
