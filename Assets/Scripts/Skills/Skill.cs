using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {

    public int unlockLevel;
    public int bonusDamagePercentagePerUse;

    // Used for skill damage calculation
    public EnemyHP enemyHPReference;

    public Sprite icon;
    public Sprite iconLocked;
    public Sprite pendingIcon;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

    [HideInInspector] public int id;
    [HideInInspector] public int cost;
    [HideInInspector] public bool locked;
    [HideInInspector] public int usedCounter;
    [HideInInspector] public new string name;
    [HideInInspector] public string description;

    public int Damage { get => CalcDamage(); }
    public int BonusDamage { get => CalcBonusDamage(); }
    public int TotalDamage { get => Damage + BonusDamage; }
    public int BonusPercentage { get => usedCounter * bonusDamagePercentagePerUse; }

    protected bool pending;
    protected BallController ballController;

    private SkillMenu skillMenu;
    private AudioSource sfxError;
    private AudioSource sfxSuccess;

    public Sprite Icon {
        get {
            if (locked)
                return iconLocked;
            else {
                if (pendingIcon && pending)
                    return pendingIcon;
                else
                    return icon;
            }
        }
    }

    protected void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        ballController = FindObjectOfType<BallController>();

        SkillData.Skill skillData = PersistentData.instance.skillData.GetSkillData(id);
        locked = skillData.locked;
        cost = skillData.cost;

        if (cost < 1)
            cost = 1;

        sfxError = GameObject.Find("SfxError").GetComponent<AudioSource>();
        sfxSuccess = GameObject.Find("SfxSpawn").GetComponent<AudioSource>();

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    protected virtual int CalcDamage() => LevelData.Level * 10;
    protected int CalcBonusDamage() => (int)(Damage * usedCounter * (1f / bonusDamagePercentagePerUse));

    public bool IncCost() {
        if (Score.instance.skillPoints > cost) {
            cost++;
            return true;
        }
        return false;
    }

    public void DecCost() {
        if (cost > 1)
            cost--;
    }

    protected void OnSaveGame() {
        SaveSkillData(id, locked, usedCounter);
    }

    protected void Action() {

        if (Score.instance.skillPoints < cost) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Nicht genug Skillpunkte!");
            return;
        }

        if (pending) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Skill im vollen Gange!");
            return;
        }

        if (Score.instance.PaySkillPoints(cost)) {
            pending = true;
            sfxSuccess.Play();
            usedCounter++;
            Statistics.Instance.skills.skillPointsSpend += cost;
            LevelData.DangerLevel -= cost;
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

        Action();
        barSlot.UpdateSlot();
    }

    public void Unlock() {
        locked = false;
    }

    protected void SaveSkillData(int id, bool locked, int usedCounter) {
        PersistentData.instance.skillData.SetSkillData(id, locked, usedCounter, cost);
    }

    public void ResetData() {
        locked = true;
        usedCounter = 0;
        cost = 1;
    }
}
