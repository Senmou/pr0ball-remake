using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_A : Skill {

    [SerializeField] private BigBall bigBallPrefab;
    [SerializeField] private GameObject triggeredText;

    private Canvas canvas;
    private AudioSource triggeredAudio;

    private new void Awake() {
        base.Awake();
        triggeredAudio = GameObject.Find("SfxTriggered").GetComponent<AudioSource>();
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start() {
        coolDown = 0;
        unlockLevel = 1;

        description = "Der Chef räumt auf.";
    }

    protected override int CalcDamage(int skillLevel) => skillLevel * 2;

    protected override IEnumerator ActionCoroutine() {

        List<BigBall> bigBalls = new List<BigBall>();

        pending = true;

        while (pending) {
            if (ballController.BallCount > 0)
                pending = false;
            yield return null;
        }

        GameObject text = Instantiate(triggeredText, new Vector2(0f, 0f), Quaternion.identity);
        triggeredAudio.Play();

        for (int i = 0; i < 1; i++) {
            BigBall bigBall = Instantiate(bigBallPrefab);
            bigBall.SetDamage(Damage);
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
        triggeredAudio.Stop();
    }
}
