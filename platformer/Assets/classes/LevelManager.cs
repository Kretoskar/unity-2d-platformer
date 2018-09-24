using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Text mScoreText;

    private int mScore = 0;

    private void Start() {
        mScoreText.text = mScore.ToString();
    }

    private void Update() {
        mScoreText.text = mScore.ToString();
    }

    public void UpdateScore(int score) {
        mScore += score;
    }
}
