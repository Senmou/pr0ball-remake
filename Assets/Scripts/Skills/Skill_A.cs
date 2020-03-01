using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_A : Skill {

    [SerializeField] private BigBall bigBallPrefab;
    [SerializeField] private GameObject triggeredText;

    private Canvas canvas;

    private new void Awake() {
        base.Awake();
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

        GameObject text = Instantiate(triggeredText, new Vector2(0, -22f), Quaternion.identity, canvas.transform);

        for (int i = 0; i < 1; i++) {
            BigBall bigBall = Instantiate(bigBallPrefab);
            bigBall.SetDamage(Damage);
            bigBalls.Add(bigBall);
        }

        while (ballController.BallCount > 0) {
            foreach (var item in bigBalls) {
                item?.MoveToTopMostEnemy();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
