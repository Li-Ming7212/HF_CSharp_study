using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject 一号球预制体;
    public Text ScoreText;
    public Button PlayAgainButton;


    public int Score = 0;
    //static int BallCount = 0;
    //public int BallNumber;

    public bool GameOver = false;

    public int NumberOfBalls = 0;
    public int MaximumBalls = 15;

    void Start() {
        InvokeRepeating("AddABall", 1.5f, 1);

        //BallCount++;
        //BallNumber = BallCount;
    }

    void Update() {
        ScoreText.text = Score.ToString();
    }

    void AddABall() {
        if (!GameOver) {
            Instantiate(一号球预制体);
            NumberOfBalls++;
            if (NumberOfBalls >= MaximumBalls) {
                GameOver = true;
                PlayAgainButton.gameObject.SetActive(true);
            }
        }

    }

    public void ClickedOnBall() {
        Score++;
        NumberOfBalls--;
    }

    public void StartGame() {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("GameController")) {
            Destroy(ball);
        }
        PlayAgainButton.gameObject.SetActive(false);
        Score = 0;
        NumberOfBalls = 0;
        GameOver = false;
    }
}
