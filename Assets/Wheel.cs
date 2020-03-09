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

    [SerializeField] private float minRotationTime;
    [SerializeField] private float maxRotationTime;

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

        isRotating = true;

        const float initialRotationSpeed = 75f;

        float timer = 0f;
        float slotHeight = 4f;
        float decreaseSpeedDelta = 0.2f;
        float rotationSpeed = initialRotationSpeed;

        float rotationSum = 0f;

        float rotationTime = Random.Range(minRotationTime, maxRotationTime);
        while (timer < rotationTime) {

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            rect.anchoredPosition += new Vector2(0f, rotationDelta);

            rotationSum += rotationDelta;

            if (rotationSum > slotHeight) {
                rotationSum -= slotHeight;
                benitrator.PlayClickSfx();
            }

            // Jump back to mimic a continous wheel
            if (rect.anchoredPosition.y >= 9.5f)
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - (6 * slotHeight));

            // Slow down over time
            rotationSpeed -= Random.Range(decreaseSpeedDelta, 2 * decreaseSpeedDelta);
            rotationSpeed = Mathf.Max(rotationSpeed, 20f);

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // calculate overshoot
        float dYAdjustment = rect.anchoredPosition.y % slotHeight;
        Vector2 targetPos = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - dYAdjustment);

        benitrator.PlayWheelStopSfx();

        float t = 0f;
        float tMax = 0.15f;
        while (t < tMax) {

            if (rect.anchoredPosition.y >= 9.5f) {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, 9.5f - (6 * slotHeight));
                targetPos -= new Vector2(0f, 24f);
            }

            // Snap slots in place
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, t / tMax);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        isRotating = false;

        const int ballSlotPos = 80;
        const int benisSlotPos = -80;
        const int damageSlotPos = -40;
        const int critChanceSlotPos = 0;
        const int critDamageSlotPos = 40;
        const int skillPointSlotPos = -120;

        int posY = (int)(10f * rect.anchoredPosition.y);

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
