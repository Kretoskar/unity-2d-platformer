using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField] private float mMaxHealth = 100;
    [SerializeField] private Image mHealthBarImage;

    [SerializeField] private float mCurrHealth;

    private void Start() {
        mCurrHealth = mMaxHealth;
        UpdateHealthBar();
    }

    private void Update() {
        handleMinusHp();
        UpdateHealthBar();  
    }

    private void handleMinusHp() {
        if(mCurrHealth < 0) {
            mCurrHealth = 0;
        }
    }

    private void UpdateHealthBar() {
        float ratio = mCurrHealth / mMaxHealth;
        mHealthBarImage.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void TakeDamage(int damage) {
        mCurrHealth -= damage;
    } 

    public void HealDamage(int damage) {
        mCurrHealth += damage;
    }

    public float GetCurrHealth() {
        return mCurrHealth;
    }
}
