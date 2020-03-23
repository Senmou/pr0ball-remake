using System.Collections;
using UnityEngine;
using System.Linq;

public enum SlotType {
    Damage,
    CritChance,
    CritDamage,
    Ball,
    SkillPoint,
    Score
}

public struct WheelSlot {
    public float pos;
    public SlotType type;
}

public class Wheel : MonoBehaviour {

    [SerializeField] private int fullTurns;

    [HideInInspector] public bool isRotating;

    private Transform[] slots;
    private RectTransform rect;
    private Benitrator benitrator;

    private WheelSlot[] wheelSlots;

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

    private void Start() {
        wheelSlots = new WheelSlot[]{
            new WheelSlot{ type = SlotType.Ball, pos = 4.5f},
            new WheelSlot{ type = SlotType.Damage, pos = -7.5f},
            new WheelSlot{ type = SlotType.Score, pos = -3.5f},
            new WheelSlot{ type = SlotType.SkillPoint, pos = 0.5f},
            new WheelSlot{ type = SlotType.CritDamage, pos = 8.5f},
            new WheelSlot{ type = SlotType.CritChance, pos = -11.5f},
        };
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

        float slotHeight = 4f;
        float rotationSum = 0f;

        float maxRotationTime = 6f;
        float rotationTimeCounter = 0f;

        // Rotate for some full turns
        int turns = 0;
        while (turns < fullTurns) {

            // To prevent the endless spinning bug
            rotationTimeCounter += Time.unscaledDeltaTime;
            if (rotationTimeCounter >= maxRotationTime)
                break;

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            rect.anchoredPosition += new Vector2(0f, rotationDelta);

            // Click sound
            rotationSum += rotationDelta;
            if (rotationSum > slotHeight) {
                benitrator.PlayClickSfx();
                rotationSum -= slotHeight;
            }

            // Slow down
            if (turns == fullTurns - 1) {
                rotationSpeed *= 0.985f;
                rotationSpeed = Mathf.Max(45f, rotationSpeed);
            }

            // Jump back to mimic a continous wheel
            if (rect.anchoredPosition.y >= 8.5f) {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -15.5f);
                turns++;
            }

            yield return null;
        }

        int randomSlotID = Random.Range(0, 6);
        WheelSlot targetSlot = wheelSlots[randomSlotID];

        // When crit chance is maxed out, you'll get score instead
        if (targetSlot.type == SlotType.CritChance) {
            if (BallStats.Instance.critChance >= 100f) {
                targetSlot = wheelSlots.First(x => x.type == SlotType.Score);
            }
        }

        // Chance for extra balls is increased while you have less than 5
        if(BallStats.Instance.ballCount < 5) {
            if (Random.value < 0.1f)
                targetSlot = wheelSlots.First(x => x.type == SlotType.Ball);
        }

        // Rotating to the target symbol
        while (!rect.anchoredPosition.y.Approx(targetSlot.pos, 1f)) {

            // To prevent the endless spinning bug
            rotationTimeCounter += Time.unscaledDeltaTime;
            if (rotationTimeCounter >= maxRotationTime)
                break;

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            rect.anchoredPosition += new Vector2(0f, rotationDelta);

            // Click sound
            rotationSum += rotationDelta;
            if (rotationSum > slotHeight) {
                benitrator.PlayClickSfx();
                rotationSum -= slotHeight;
            }

            // Slow down
            rotationSpeed *= 0.95f;
            rotationSpeed = Mathf.Max(30f, rotationSpeed);

            // Jump back to mimic a continous wheel
            if (rect.anchoredPosition.y >= 8.5f)
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -15.5f);

            yield return null;
        }

        // Snapping into position
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, targetSlot.pos);

        benitrator.PlayWheelStopSfx();

        isRotating = false;

        onStoppedRotating(targetSlot.type, gameObject.name);
    }
}
