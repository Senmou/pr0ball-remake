﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Skill_C : Skill {

    [SerializeField] private int damage;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip[] hitAudioClips;
    [SerializeField] private GameObject hammerTimeLogo;
    [SerializeField] private GameObject onHitParticleSystem;

    public GameObject hammer;
    public ContactFilter2D contactFilter;
    private BoxCollider2D hammerCollider;
    private AudioSource audioSource;

    private new void Awake() {
        base.Awake();
        hammer = Instantiate(hammer);
        hammerCollider = hammer.GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        hammer.SetActive(false);

        hammerTimeLogo = Instantiate(hammerTimeLogo, new Vector3(-30f, 9f), Quaternion.identity);
    }

    protected override IEnumerator ActionCoroutine() {

        float xPos = Random.Range(-8f, 7.4f);
        hammer.transform.position = new Vector3(xPos, 26f);
        Vector2 startPos = hammer.transform.position;
        Vector2 targetPos = startPos + new Vector2(0f, -52f);

        List<BaseEnemy> hitEnemies = new List<BaseEnemy>();

        hammer.SetActive(true);
        float fallingSpeed = 1.5f;

        int randomAudioClip = Random.Range(0, audioClips.Length - 1);
        audioSource.clip = audioClips[randomAudioClip];
        audioSource.Play();

        StopCoroutine("ShowHammerTimeLogo");
        StartCoroutine(ShowHammerTimeLogo());

        while (Vector2.Distance(hammer.transform.position, targetPos) > 0.1f) {
            hammer.transform.position = Vector2.MoveTowards(hammer.transform.position, targetPos, fallingSpeed * Time.deltaTime);
            if (!GameController.isGamePaused)
                fallingSpeed += 0.7f;

            Collider2D[] hits = new Collider2D[16];
            int numHits = Physics2D.OverlapCollider(hammerCollider, contactFilter, hits);

            for (int i = 0; i < numHits; i++) {
                BaseEnemy hitEnemy = hits[i].GetComponentInParent<BaseEnemy>();

                if (hitEnemy && hitEnemy.canTakeDamageFromSkill) {
                    int randomHitAudioClip = Random.Range(0, hitAudioClips.Length - 1);
                    audioSource.PlayOneShot(hitAudioClips[randomHitAudioClip]);

                    Instantiate(onHitParticleSystem, hitEnemy.transform.position, Quaternion.identity);

                    hitEnemy.TakeDamage(damage);
                    hitEnemy.canTakeDamageFromSkill = false;
                    hitEnemies.Add(hitEnemy);
                }
            }

            yield return null;
        }

        foreach (var enemy in hitEnemies) {
            enemy.canTakeDamageFromSkill = true;
        }

        pending = false;

        yield return null;
    }

    private IEnumerator ShowHammerTimeLogo() {

        Vector3 startPos = new Vector3(-30f, 9f);
        Vector3 midPos = new Vector3(0f, 9f);
        Vector3 endPos = new Vector3(30f, 9f);

        hammerTimeLogo.transform.position = startPos;
        while (Vector2.Distance(hammerTimeLogo.transform.position, midPos) > 0.1f) {
            hammerTimeLogo.transform.position = Vector2.Lerp(hammerTimeLogo.transform.position, midPos, 12f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (Vector2.Distance(hammerTimeLogo.transform.position, endPos) > 0.1f) {
            hammerTimeLogo.transform.position = Vector2.Lerp(hammerTimeLogo.transform.position, endPos, 12f * Time.deltaTime);
            yield return null;
        }
    }
}
