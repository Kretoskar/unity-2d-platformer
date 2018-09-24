using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    [SerializeField] private int mDamage = 50;

    private PlayerStats mPlayerStats;

    private void Start() {
        mPlayerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        castDamage(mDamage);
    }

    private void castDamage(int damage) {
        mPlayerStats.TakeDamage(damage);
    }
}
