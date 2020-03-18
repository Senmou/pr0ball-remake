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

    [SerializeField] private int fullTurns;

    [HideInInspector] public bool isRotating;

    private Transform[] slots;
    private RectTransform rect;
    private Benitrator benitrator;

    private void Awake() {

        rect = GetComponent<RectTransform>();
        benitrator = FindObjectOfType<Benitrator>();

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

        float initialRotationSpeed = 75f;
        float rotationSpeed = initialRotationSpeed;

        isRotating = true;

        // Rotating for some time
        float t = 0f;
        bool shouldRotate = true;
        float rotateForSeconds = Random.Range(0.25f, 1.25f);

        int turns = 0;
        while(turns < fullTurns) {

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            rect.anchoredPosition += new Vector2(0f, rotationDelta);

            //Jump back to mimic a continous wheel
            if (rect.anchoredPosition.y >= 8.5f) {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -15.5f);
                turns++;
            }

            yield return null;
        }

        //while (shouldRotate) {

        //    float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
        //    rect.anchoredPosition += new Vector2(0f, rotationDelta);

        //    //Jump back to mimic a continous wheel
        //    if (rect.anchoredPosition.y >= 8.5f)
        //        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -15.5f);

        //    t += Time.unscaledDeltaTime;
        //    if (t >= rotateForSeconds)
        //        shouldRotate = false;

        //    yield return null;
        //}

        float[] symbolPos = { -11.5f, -7.5f, -3.5f, 0.5f, 4.5f, 8.5f };
        float targetSymbolPos = symbolPos[Random.Range(0, 6)];

        // Rotating to the target symbol
        while (!rect.anchoredPosition.y.Approx(targetSymbolPos, 1f)) {

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            rect.anchoredPosition += new Vector2(0f, rotationDelta);

            //Jump back to mimic a continous wheel
            if (rect.anchoredPosition.y >= 8.5f)
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -15.5f);

            yield return null;
        }

        // Snapping into position
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, targetSymbolPos);
        
        benitrator.PlayWheelStopSfx();

        isRotating = false;

        // -11.5 crit Chance
        //-8 dmg
        //-3.5 Benis
        //0.5 skillpoint
        //4.5 balls
        //8.5 crit DMG
        //8.5 jump -> -15.5

        const float ballSlotPos = 4.5f;
        const float benisSlotPos = -3.5f;
        const float damageSlotPos = -8f;
        const float critChanceSlotPos = -11.5f;
        const float critDamageSlotPos = 8.5f;
        const float skillPointSlotPos = 0.5f;

        float posY = rect.anchoredPosition.y;

        SlotType result = new SlotType();

        if (posY == damageSlotPos) {
            result = SlotType.Damage;
        } else if (posY == critChanceSlotPos) {
            result = SlotType.CritChance;
        } else if (posY == critDamageSlotPos) {
            result = SlotType.CritDamage;
        } else if (posY == ballSlotPos) {
            result = SlotType.Ball;
        } else if (posY == skillPointSlotPos) {
            result = SlotType.SkillPoint;
        } else if (posY == benisSlotPos) {
            result = SlotType.Score;
        }

        onStoppedRotating(result, gameObject.name);
    }
}
