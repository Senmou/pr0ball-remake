using UnityEngine;

public class DEBUG_WHEEL : MonoBehaviour {

    public int counter;
    public int fullTurns;
    public int slotRotations;

    public int damage;
    public int critChance;
    public int critDamage;
    public int balls;
    public int skillPoints;
    public int score;

    public int Total { get => damage + critChance + critDamage + balls + skillPoints + score; }

    public float pDamage;
    public float pCritChance;
    public float pCritDamage;
    public float pBalls;
    public float pSkillPoints;
    public float pScore;

    public float a;
    public float b;

    private void Update() {

        if (Total > 0) {
            pDamage = 100f * (float)damage / Total;
            pCritChance = 100f * (float)critChance / Total;
            pCritDamage = 100f * (float)critDamage / Total;
            pBalls = 100f * (float)balls / Total;
            pSkillPoints = 100f * (float)skillPoints / Total;
            pScore = 100f * (float)score / Total;
        }

        if (Input.GetKey(KeyCode.K)) {
            Rotate();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Rotate();
        }
    }

    private void Rotate() {

        float slotHeight = 4f;

        int randomSlotNumber = slotRotations;//Random.Range(0, 6);
        int totalSlotRotations = (6 * fullTurns) + randomSlotNumber;

        float startPosY = 0f;
        float totalRotationDistance = totalSlotRotations * slotHeight;

        float rotationSum = 0f;
        float initialRotationSpeed = 75f;
        float rotationSpeed = initialRotationSpeed;
        int slotRotationCounter = 0;

        while (slotRotationCounter < totalSlotRotations) {

            float rotationDelta = rotationSpeed * Time.unscaledDeltaTime;
            startPosY += rotationDelta;

            rotationSum += rotationDelta;

            // Check if wheel rotated one full slot
            if (rotationSum > slotHeight) {
                slotRotationCounter++;
                rotationSum -= slotHeight;
            }

            // Jump back to mimic a continous wheel
            if (startPosY >= 9.5f)
                startPosY -= 6 * slotHeight;

            // Slow down over time
            rotationSpeed -= 0.2f;
            rotationSpeed = Mathf.Max(rotationSpeed, 30f);
        }

        // Calculate overshoot
        float dYAdjustment = startPosY % slotHeight;
        float targetPos = startPosY - dYAdjustment;
        a = targetPos;
        const int ballSlotPos = 80;
        const int benisSlotPos = -80;
        const int damageSlotPos = -40;
        const int critChanceSlotPos = 0;
        const int critDamageSlotPos = 40;
        const int skillPointSlotPos = -120;

        int posY = (int)(10f * targetPos);
        b = posY;

        if (posY == damageSlotPos) {
            damage++;
        } else if (posY == critChanceSlotPos) {
                critChance++;
        } else if (posY == critDamageSlotPos) {
            critDamage++;
        } else if (posY == ballSlotPos) {
            balls++;
        } else if (posY == skillPointSlotPos) {
            skillPoints++;
        } else if (posY == benisSlotPos) {
            score++;
        }

        counter++;
    }
}
