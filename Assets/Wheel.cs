using System.Collections;
using UnityEngine;

public enum SlotType {
    Damage,
    CritChance,
    CritDamage,
    Ball,
    SkillPoint,
    Score
}

public class Wheel : MonoBehaviour {

    // How long should the wheel rotate?
    [SerializeField] int fullTurnsBeforeStop;

    [HideInInspector] public bool isRotating;

    private Transform[] slots;
    private RectTransform rect;

    private void Awake() {

        rect = GetComponent<RectTransform>();

        int childCount = transform.childCount;
        slots = new Transform[transform.childCount * 2];

        for (int i = 0; i < childCount; i++) {
            slots[i] = transform.GetChild(i);
        }

        // Duplicate all slots to get a seamless loop
        for (int i = 0; i < childCount; i++) {
            Instantiate(transform.GetChild(i), transform);
        }

        StartCoroutine(SetPositionDelayed());
    }

    private IEnumerator SetPositionDelayed() {
        yield return new WaitForEndOfFrame();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -3.5f);
    }

    public void StartRotating(Benitrator.ResultDelegate onStoppedRotating) {
        StartCoroutine(Rotate(onStoppedRotating));
    }

    private IEnumerator Rotate(Benitrator.ResultDelegate onStoppedRotating) {

        isRotating = true;

        float deltaY = 0.5f;
        float slotHeight = 4f;

        // How many slots should be cycled through after slowing down?
        int stepCount = UnityEngine.Random.Range(0, 6) * (int)(slotHeight / deltaY);

        // How often is deltaY applied to the wheels posY
        int stepCountTotal = fullTurnsBeforeStop * 6 * (int)(slotHeight / deltaY) + stepCount;

        int posY = 0;
        for (int i = 0; i < stepCountTotal; i++) {

            rect.anchoredPosition += new Vector2(0f, deltaY);

            // int cast to avoid floating point errors
            posY = (int)(rect.anchoredPosition.y * 10f);
            if (posY == 125f)
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, 12.5f - 24f);

            yield return null;
        }

        /**
        * posY
        * -35 Damage
        * 5 Crit
        * 45 CritDamage
        * 85 Balls
        * 125 SkillPoint
        * -75 Benis
        * 
        **/

        isRotating = false;

        SlotType result = new SlotType();

        if (posY == -35) {
            result = SlotType.Damage;
        } else if (posY == 5) {
            result = SlotType.CritChance;
        } else if (posY == 45) {
            result = SlotType.CritDamage;
        } else if (posY == 85) {
            result = SlotType.Ball;
        } else if (posY == 125) {
            result = SlotType.SkillPoint;
        } else if (posY == -75) {
            result = SlotType.Score;
        }

        onStoppedRotating(result, gameObject.name);
    }
}
