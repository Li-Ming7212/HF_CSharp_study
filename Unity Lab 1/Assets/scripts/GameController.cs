using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject 一号球预制体;
    static int BallCount = 0;
    public int BallNumber;

    void Start() {
        InvokeRepeating("AddABall", 1.5f, 1);

        BallCount++;
        BallNumber = BallCount;
    }

    void Update() { }

    void AddABall() {
        Instantiate(一号球预制体);
    }
}
