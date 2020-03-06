using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {

    // Used for skill damage calculation
    public EnemyHP enemyHPReference;

    public int id;
    public int cost;
    public bool locked;
    public int unlockLevel;
    public int usedCounter;
    public new string name;
    public string description;

    public Sprite icon;
    public Sprite iconLocked;
    public Sprite pendingIcon;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;
    public int Damage { get => CalcDamage(); }
    public int BonusDamage { get => CalcBonusDamage(); }
    public int TotalDamage { get => Damage + BonusDamage; }

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

        sfxError = GameObject.Find("SfxError").GetComponent<AudioSource>();
        sfxSuccess = GameObject.Find("SfxSpawn").GetComponent<AudioSource>();

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    protected virtual int CalcDamage() => LevelData.Level * 10;
    protected int CalcBonusDamage() => (int)(Damage * usedCounter * 0.05f);

    protected void OnSaveGame() {
        SaveSkillData(id, locked, usedCounter);
    }

    protected void Action() {

        if (Score.instance.skillPoints == 0) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Nicht genug Skillpunkte!");
        }

        if (pending) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Skill im vollen Gange!");
        }

        if (!pending && Score.instance.PaySkillPoints(cost)) {
            pending = true;
            sfxSuccess.Play();
            usedCounter++;
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
        PersistentData.instance.skillData.SetSkillData(id, locked, usedCounter);
    }

    public void ResetData() {
        locked = true;
        usedCounter = 0;
    }
}
