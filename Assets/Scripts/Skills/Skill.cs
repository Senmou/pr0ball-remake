using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {

    public int unlockLevel;
    public int bonusDamagePercentagePerPaidSkillPoint;

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
    [HideInInspector] public int paidCost;
    [HideInInspector] public string title;
    [HideInInspector] public bool hasToken;
    [HideInInspector] public int usedCounter;
    [HideInInspector] public new string name;
    [HideInInspector] public bool usedThisTurn;

    public int BonusPercentage { get => skillPointsSpend * bonusDamagePercentagePerPaidSkillPoint; }

    public int GetDamage(int paidCost) => CalcDamage(paidCost);
    public int GetBonusDamage(int paidCost) => (int)(GetDamage(paidCost) * (BonusPercentage / 100f));
    public int GetTotalDamage(int paidCost) => (int)(GetDamage(paidCost) * (1f + BonusPercentage / 100f));

    protected bool pending;
    protected BallController ballController;

    private int skillPointsSpend;
    private SkillMenu skillMenu;
    private AudioSource sfxError;
    private AudioSource sfxSuccess;

    public Sprite Icon { get => locked ? iconLocked : icon; }

    protected void Awake() {

        skillMenu = FindObjectOfType<SkillMenu>();
        ballController = FindObjectOfType<BallController>();
        sfxError = GameObject.Find("SfxError").GetComponent<AudioSource>();
        sfxSuccess = GameObject.Find("SfxSpawn").GetComponent<AudioSource>();

        SkillData.Skill skillData = PersistentData.instance.skillData.GetSkillData(id);
        skillPointsSpend = skillData.skillPointsSpend;
        usedThisTurn = skillData.usedThisTurn;
        hasToken = skillData.hasToken;
        cost = skillData.cost;

        EventManager.StartListening("ChacheData", OnChanceData);
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);

        locked = false;
    }

    private void Start() {
        barSlot.UpdateSlot();
    }

    protected virtual int CalcDamage(int cost) => LevelData.Level * 10;

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

    protected void OnChanceData() {
        SaveSkillData(id, locked, usedCounter, cost, hasToken, usedThisTurn, skillPointsSpend);
    }

    protected void OnReachedNextLevel() {
        usedThisTurn = false;
    }

    protected void Action() {
        
        if (hasToken == false) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Kein Token!");
            return;
        }

        if (pending) {
            sfxError.Play();
            ErrorMessage.instance.Show(1f, "Skill im vollen Gange!");
            return;
        }
        
        if (hasToken) {
            hasToken = false;
            pending = true;
            usedCounter++;
            sfxSuccess.Play();
            barSlot.UpdateSlot();
            StartCoroutine(ActionCoroutine());
        }
    }

    public void UpdateCost() {
        if (cost > Score.instance.skillPoints)
            cost = Mathf.Max(1, Score.instance.skillPoints);
    }

    protected virtual IEnumerator ActionCoroutine() {
        //Debug.Log("Skill used: " + name);
        yield return null;
    }

    public virtual void UseSkill() {

        if (skillMenu.isVisible)
            return;

        Action();
    }

    public void Unlock() {
        locked = false;
    }

    protected void SaveSkillData(int id, bool locked, int usedCounter, int cost, bool hasToken, bool usedThisTurn, int skillPointsSpend) {
        PersistentData.instance.skillData.SetSkillData(id, locked, usedCounter, cost, hasToken, usedThisTurn, skillPointsSpend);
    }

    public void ResetData() {
        usedCounter = 0;
        hasToken = false;
    }
}
