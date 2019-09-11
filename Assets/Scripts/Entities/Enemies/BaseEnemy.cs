﻿using MarchingBytes;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {


    [HideInInspector] public long maxHP;
    [HideInInspector] public long currentHP;
    [HideInInspector] public int benisValue;
    [HideInInspector] public Rigidbody2D body;

    protected WaveStateController waveStateController;

    private Transform deadline;
    private EnemyController enemyController;
    private TextMeshProUGUI healthPointUI;
    private PlayerHP playerHP;

    protected void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        waveStateController = FindObjectOfType<WaveStateController>();
        enemyController = FindObjectOfType<EnemyController>();
        healthPointUI = GetComponentInChildren<TextMeshProUGUI>();
        playerHP = FindObjectOfType<PlayerHP>();
        deadline = GameObject.Find("Deadline").transform;
    }

    private void Start() {
        UpdateUI();
    }

    private void Update() {
        if (transform.position.y >= deadline.position.y) {
            ReturnToPool(this);
            playerHP.TakeDamage(1);
        }
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
