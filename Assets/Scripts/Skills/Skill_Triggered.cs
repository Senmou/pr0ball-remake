using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Skill_Triggered : Skill {

    [SerializeField] private BigBall bigBallPrefab;
    [SerializeField] private GameObject triggeredText;

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
        description = "Listen here, you little shit";
    }

    protected override int CalcDamage() => enemyHPReference.MaxHP;

    protected override IEnumerator ActionCoroutine() {

        Statistics.Instance.skills.skill_3.used++;

        List<BigBall> bigBalls = new List<BigBall>();

        pending = true;

        while (pending) {
            if (ballController.BallCount > 0) {
                pending = false;
                barSlot.UpdateSlot();
            }
            yield return null;
        }

        GameObject text = Instantiate(triggeredText, new Vector2(0f, 0f), Quaternion.identity);
        triggeredAudio.Play();
        MusicController.instance.SetVolume(0f);
        audioSource.Play();

        for (int i = 0; i < 1; i++) {
            BigBall bigBall = Instantiate(bigBallPrefab);
            bigBall.SetDamage(TotalDamage);
            bigBalls.Add(bigBall);
        }

        while (ballController.BallCount > 0) {
            foreach (var item in bigBalls) {
                item?.MoveToTopMostEnemy();
            }
            CameraEffect.instance.Shake(0.05f, 0.7f, true);
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(text);
        audioSource.Stop();
        triggeredAudio.Stop();
        MusicController.instance.RestoreLastVolume();
    }
}
