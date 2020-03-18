﻿using System.Collections;
using UnityEngine;

public class Skill_Triggered : Skill {

    [SerializeField] private GameObject triggeredText;
    [SerializeField] private FogelBall fogelBallPrefab;

    private Canvas canvas;
    private AudioSource audioSource;
    private AudioSource triggeredAudio;

    private new void Awake() {
        base.Awake();
        canvas = FindObjectOfType<Canvas>();
        audioSource = GetComponent<AudioSource>();
        triggeredAudio = GameObject.Find("SfxTriggered").GetComponent<AudioSource>();
    }

    private void Start() {
        description = "Gemeiner Virus";
    }

    protected override int CalcDamage() => cost + cost * (enemyHPReference.MaxHP / 20);

    protected override IEnumerator ActionCoroutine() {

        Statistics.Instance.skills.skill_3.used++;

        GameObject text = Instantiate(triggeredText, new Vector2(0f, 0f), Quaternion.identity);
        triggeredAudio.Play();
        MusicController.instance.SetVolume(0f);
        audioSource.Play();

        FogelBall fogelBall = Instantiate(fogelBallPrefab);
        fogelBall.SetDamage(TotalDamage);

        float t = 6f;
        float dashInterval = 0.05f;
        while (t > 0f) {
            t -= dashInterval;
            fogelBall.MoveToTopMostEnemy();
            CameraEffect.instance.Shake(0.05f, 0.7f, true);
            yield return new WaitForSeconds(dashInterval);
        }

        pending = false;

        Destroy(text);
        Destroy(fogelBall.gameObject);

        audioSource.Stop();
        triggeredAudio.Stop();

        MusicController.instance.RestoreLastVolume();
    }
}
