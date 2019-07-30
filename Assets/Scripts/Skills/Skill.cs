using UnityEngine;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int coolDownCounter;
    public new string name;
    public bool isUnlocked;

    public Sprite icon;

    public SkillBarSlot slot;

    private void Awake() {
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    public void OnWaveCompleted() {
        if (coolDownCounter > 0) {
            coolDownCounter--;
            slot.UpdateSlot();
        }
    }

    public virtual void UseSkill() {
        if (coolDownCounter == 0) {
            Debug.Log("Used: " + name);
            ResetCoolDown();
            slot.UpdateSlot();
        } else
            Debug.Log("skill is on CD");
    }

    public void ResetCoolDown() {
        coolDownCounter = coolDown;
    }
}
