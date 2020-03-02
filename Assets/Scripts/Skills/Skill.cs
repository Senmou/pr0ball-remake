using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {

    public int id;
    public int coolDown;
    public int remainingCoolDown;
    public new string name;
    public bool locked;
    public int skillLevel;
    public int unlockLevel;
    public string description;

    protected bool pending;

    public int UpgradePrice { get => CalcUpgradePrice(skillLevel); }
    public int Damage { get => CalcDamage(skillLevel); }

    public Sprite icon;
    public Sprite iconLocked;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

    protected BallController ballController;

    private SkillMenu skillMenu;
    private AudioSource sfxError;

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

        sfxError = GameObject.Find("SfxError").GetComponent<AudioSource>();

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }
    private int CalcUpgradePrice(int skillLevel) => skillLevel;
    protected virtual int CalcDamage(int skillLevel) => skillLevel * 10;

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

        if (Score.instance.skillPoints == 0) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f);
        }

        if (!pending && Score.instance.PaySkillPoints(1)) {
            pending = true;
            StartCoroutine(ActionCoroutine());
        }
    }

    protected virtual IEnumerator ActionCoroutine() {
        //Debug.Log("Skill used: " + name);
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

    public void ResetData() {
        remainingCoolDown = 0;
        locked = true;
        skillLevel = 1;
    }
}
