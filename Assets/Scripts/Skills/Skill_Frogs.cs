using System.Collections;
using MarchingBytes;
using UnityEngine;

public class Skill_Frogs : Skill {

    private int frogCount = 5;

    private const string frogPoolName = "Frog_pool";

    protected override int CalcDamage(int cost) => enemyHPReference.MaxHP;

    private new void Awake() {
        base.Awake();
        title = "Quak";
        description = "-2% Gefahrenlevel pro Blussi";
        dangerLevelReduction = 10;
        tokenCost = 2;
    }

    protected override IEnumerator ActionCoroutine() {

        LevelData.DangerLevel -= dangerLevelReduction;

        Statistics.Instance.skills.skill_2.used++;
        Statistics.Instance.skills.skill_2.skillPointsSpend += paidCost;

        for (int i = 0; i < frogCount; i++) {
            float posY = 23f;
            float posX = Random.Range(-9.36f, 9.36f);
            Frog frog = EasyObjectPool.instance.GetObjectFromPool(frogPoolName, new Vector3(posX, posY), Quaternion.identity).GetComponent<Frog>();
            frog.SetDamage(GetTotalDamage(paidCost));
            CameraEffect.instance.Shake(0.1f, 0.15f);
            yield return new WaitForSeconds(0.3f);
        }

        pending = false;

        yield return null;
    }
}
