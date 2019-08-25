using UnityEngine;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int coolDownCounter;
    public new string name;
    public bool locked;
    public int price;
    public int skillLevel;

    public Sprite icon;
    public Sprite iconLocked;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

    public Sprite Icon {
        get {
            if (locked)
                return iconLocked;
            else
                return icon;
        }
    }

    private void Awake() {
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
        locked = true;
        skillLevel = 0;
    }

    public void OnWaveCompleted() {
        if (coolDownCounter > 0) {
            coolDownCounter--;
            barSlot.UpdateSlot();
        }
    }

    public void IncSkill() {
        if (locked)
            locked = false;
        skillLevel++;
    }

    public virtual void UseSkill() {
        if (coolDownCounter == 0) {
            Debug.Log("Used: " + name);
            ResetCoolDown();
            barSlot.UpdateSlot();
        } else
            Debug.Log("skill is on CD");
    }

    public void Unlock() {
        locked = false;
    }

    public void ResetCoolDown() {
        coolDownCounter = coolDown;
    }
}
