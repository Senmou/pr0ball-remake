using System.Collections;
using MarchingBytes;
using UnityEngine;

public class Skill_Frogs : Skill {

    private int frogCount = 3;

    private const string frogPoolName = "Frog_pool";

    protected override int CalcDamage(int skillLevel) => 15 * skillLevel;

    private new void Awake() {
        base.Awake();
        coolDown = 0;
        description = "Es regnet hochexplosive Bergfestzelebrierungsfrösche. Verursachen Bereichsschaden.";
    }

    protected override IEnumerator ActionCoroutine() {

        for (int i = 0; i < frogCount; i++) {
            float posY = 23f;
            float posX = Random.Range(-9.36f, 9.36f);
            Frog frog = EasyObjectPool.instance.GetObjectFromPool(frogPoolName, new Vector3(posX, posY), Quaternion.identity).GetComponent<Frog>();
            frog.SetDamage(Damage);
            CameraEffect.instance.Shake(0.1f, 0.15f);
            yield return new WaitForSeconds(0.3f);
        }

        pending = false;

        yield return null;
    }
}
