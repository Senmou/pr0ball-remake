using UnityEngine;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int coolDownCounter;
    public new string name;
    public bool locked;
    public int skillLevel;
    public int unlockLevel;

    public int UpgradePrice { get => CalcUpgradePrice(skillLevel); }

    public Sprite icon;
    public Sprite iconLocked;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

    private SkillMenu skillMenu;

    public Sprite Icon {
        get {
            if (locked)
                return iconLocked;
            else
                return icon;
        }
    }

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);

        locked = PersistentData.instance.skillData.GetSkillData(id).locked;
        skillLevel = PersistentData.instance.skillData.GetSkillData(id).level;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }
    
    private int CalcUpgradePrice(int skillLevel) => skillLevel;

    protected void OnSaveGame() {
        SaveSkillData(id, skillLevel, locked);
    }

    public void OnWaveCompleted() {
        if (coolDownCounter > 0) {
            coolDownCounter--;
            barSlot.UpdateSlot();
        }
    }

    public virtual void UseSkill() {

        if (skillMenu.isVisible)
            return;

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

    protected void SaveSkillData(int id, int level, bool locked) {
        PersistentData.instance.skillData.SetSkillData(id, level, locked);
    }
}
