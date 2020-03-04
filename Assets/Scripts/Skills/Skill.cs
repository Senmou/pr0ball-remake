using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {

    // Used for skill damage calculation
    public EnemyHP enemyHPReference;

    public int id;
    public int coolDown;
    public int remainingCoolDown;
    public new string name;
    public bool locked;
    public int unlockLevel;
    public string description;
    public Sprite pendingIcon;

    protected int cost;
    protected bool pending;

    public int Damage { get => CalcDamage(LevelData.Level); }

    public Sprite icon;
    public Sprite iconLocked;
    public SkillBarSlot barSlot;
    public SkillMenuSlot menuSlot;

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
        remainingCoolDown = skillData.remainingCoolDown;

        sfxError = GameObject.Find("SfxError").GetComponent<AudioSource>();
        sfxSuccess = GameObject.Find("SfxSpawn").GetComponent<AudioSource>();

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }
    
    protected virtual int CalcDamage(int level) => level * 10;

    protected void OnSaveGame() {
        SaveSkillData(id, locked, remainingCoolDown);
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
            ErrorMessage.instance.Show(1f, "Nicht genug Skillpunkte!");
        }

        if (pending) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Skill im vollen Gange!");
        }

        if (!pending && Score.instance.PaySkillPoints(1)) {
            pending = true;
            sfxSuccess.Play();
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

    protected void SaveSkillData(int id, bool locked, int remainingCoolDown) {
        PersistentData.instance.skillData.SetSkillData(id, locked, remainingCoolDown);
    }

    public void ResetData() {
        remainingCoolDown = 0;
        locked = true;
    }
}
