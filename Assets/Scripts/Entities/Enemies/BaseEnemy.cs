﻿using MarchingBytes;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    [HideInInspector] public long maxHP;
    [HideInInspector] public long currentHP;
    [HideInInspector] public int benisValue;
    [HideInInspector] public Rigidbody2D body;

    private TextMeshProUGUI healthPointUI;
    private EnemyController enemyController;

    protected void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateUI();
    }
    
    public void SetData() {
        currentHP = maxHP;
        UpdateUI();
    }

    private void ReturnToPool(BaseEnemy enemy) {
        enemyController.activeEnemies.Remove(enemy);

        if (gameObject.activeSelf)
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    private void TakeDamage(int amount) {
        currentHP -= amount;
        UpdateUI();
        if (currentHP <= 0) {
            ReturnToPool(this);
            Score.instance.IncScore(benisValue);
        }
    }

    public void UpdateUI() {
        healthPointUI.text = currentHP.ToStringFormatted();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        TakeDamage(ball.Damage());
    }
}
