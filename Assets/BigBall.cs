﻿using UnityEngine;

public class BigBall : MonoBehaviour {

    [SerializeField] private GameObject onHitParticleSystem;

    private AudioSource audioSource;

    [HideInInspector] public Rigidbody2D body;
    private float maxVelocity = 100f;

    private TrailRenderer trailRenderer;
    private int damage;

    private void OnEnable() {
        body.gravityScale = 0f;
    }

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material.color = new Color(1, 0.4829951f, 0f, 1f); // orange
        audioSource = GameObject.Find("SfxOrangeBallHit").GetComponent<AudioSource>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    public void SetDamage(int value) {
        damage = value;
    }

    private void OnWaveCompleted() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        body.gravityScale = 5f;

        if (other.gameObject.CompareTag("Enemy")) {
            audioSource.PlayOneShot(audioSource.clip);
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damage);
            Instantiate(onHitParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
