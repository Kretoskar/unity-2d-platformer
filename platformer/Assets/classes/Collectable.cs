using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] private int mValue = 10;
    [SerializeField] private AudioClip coinPickupSfx;

    private LevelManager mLevelManager;

    private void Start() {
        mLevelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            mLevelManager.UpdateScore(mValue);
            PlaySound();
            Destroy(gameObject);
        }
    }

    private void PlaySound() {
        AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
    }
}
