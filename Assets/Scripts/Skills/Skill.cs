using UnityEngine;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int remainingCoolDown;
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

    protected void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);

        SkillData.Skill skillData = PersistentData.instance.skillData.GetSkillData(id);
        locked = skillData.locked;
        skillLevel = skillData.level;
        remainingCoolDown = skillData.remainingCoolDown;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }
    
    private int CalcUpgradePrice(int skillLevel) => skillLevel;

    protected void OnSaveGame() {
        SaveSkillData(id, skillLevel, locked, remainingCoolDown);
    }

    public void OnWaveCompleted() {
        if (remainingCoolDown > 0) {
            remainingCoolDown--;
            barSlot.UpdateSlot();
        }
    }

    public virtual void UseSkill() {

        if (skillMenu.isVisible)
            return;

        if (remainingCoolDown == 0) {
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
        remainingCoolDown = coolDown;
    }

    protected void SaveSkillData(int id, int level, bool locked, int remainingCoolDown) {
        PersistentData.instance.skillData.SetSkillData(id, level, locked, remainingCoolDown);
    }
}
