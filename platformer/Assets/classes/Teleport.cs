using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

    [SerializeField] private int mSceneIndexToLoad = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        SceneManager.LoadScene(mSceneIndexToLoad);
    }
}
