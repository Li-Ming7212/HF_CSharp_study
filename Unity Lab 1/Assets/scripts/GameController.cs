using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject һ����Ԥ����;
    static int BallCount = 0;
    public int BallNumber;

    void Start() {
        InvokeRepeating("AddABall", 1.5f, 1);

        BallCount++;
        BallNumber = BallCount;
    }

    void Update() { }

    void AddABall() {
        Instantiate(һ����Ԥ����);
    }
}
