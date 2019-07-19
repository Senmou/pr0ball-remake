﻿using UnityEngine;

public class Ball : MonoBehaviour {

    public BallConfig ballConfigDefault;

    public float damage = 1f;
    public float critChance = 0f;
    public float critDamageMultiplier = 1f;

    [HideInInspector] public Rigidbody2D body;

    private float maxVelocity = 50f;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        ballConfigDefault.Apply(this);
    }

    private void FixedUpdate() {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.Play();
    }
}