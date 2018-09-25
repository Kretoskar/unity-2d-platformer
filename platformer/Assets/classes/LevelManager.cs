using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Text mScoreText;

    private int mScore = 0;
    private int numberOfGameSession;

    private void Start() {
        SingletonPattern();
        mScoreText.text = mScore.ToString();
    }

    private void SingletonPattern() {
        numberOfGameSession = FindObjectsOfType<LevelManager>().Length;
        if (numberOfGameSession > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {
        mScoreText.text = mScore.ToString();
    }

    public void UpdateScore(int score) {
        mScore += score;
    }
}
