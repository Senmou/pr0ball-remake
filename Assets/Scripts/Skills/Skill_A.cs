using System.Collections;
using UnityEngine;

public class Skill_A : Skill {

    [SerializeField] private BigBall bigBall;

    private new void Awake() {
        base.Awake();
        bigBall.SetDamage(Damage);
    }

    private void Start() {
        coolDown = 2;
        unlockLevel = 1;

        description = "Der Chef räumt auf.";
    }

    protected override int CalcDamage(int skillLevel) => skillLevel * 2;

    protected override IEnumerator ActionCoroutine() {

        pending = false;

        yield return null;
    }
}
