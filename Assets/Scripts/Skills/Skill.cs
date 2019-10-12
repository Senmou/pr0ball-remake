using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int remainingCoolDown;
    public new string name;
    public bool locked;
    public int skillLevel;
    public int unlockLevel;
    protected bool pending;

    public int UpgradePrice { get => CalcUpgradePrice(skillLevel); }

    public Sprite icon;
    public Sprite iconLocked;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

    private SkillMenu skillMenu;
    protected BallController ballController;

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
        ballController = FindObjectOfType<BallController>();

        SkillData.Skill skillData = PersistentData.instance.skillData.GetSkillData(id);
        locked = skillData.locked;
        skillLevel = skillData.level;
        remainingCoolDown = skillData.remainingCoolDown;

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private int CalcUpgradePrice(int skillLevel) => skillLevel;

    protected void OnSaveGame() {
        SaveSkillData(id, skillLevel, locked, remainingCoolDown);
    }

    public void OnWaveCompleted() {
        if (remainingCoolDown > 0 && !pending) {
            remainingCoolDown--;
            barSlot.UpdateSlot();
        }
    }

    protected void Action() {
        pending = true;
        StartCoroutine(ActionCoroutine());
    }

    protected virtual IEnumerator ActionCoroutine() {
        Debug.Log("Skill used: " + name);
        yield return null;
    }

    public virtual void UseSkill() {

        if (skillMenu.isVisible)
            return;

        if (remainingCoolDown == 0) {
            Action();
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
