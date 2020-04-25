using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    [HideInInspector] public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    //private SkillMenuUnlockButton unlockButton;

    //private Button plus;
    //private Button minus;
    //private TextMeshProUGUI costUI;
    private TextMeshProUGUI titleUI;
    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI descriptionUI;
    private TextMeshProUGUI usedCounterUI;
    private TextMeshProUGUI bonusDamageUI;
    //private TextMeshProUGUI dangerLevelIncreaseUI;

    private Transform infoCost;
    private Transform infoDamage;
    private Transform damageIcon;
    private Transform usedCounterIcon;
    private Transform infoUsedCounter;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        image = transform.FindChild<Image>("Icon");
        //unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        infoCost = transform.FindChild<Transform>("InfoPopups/Cost");
        infoDamage = transform.FindChild<Transform>("InfoPopups/Damage");
        infoUsedCounter = transform.FindChild<Transform>("InfoPopups/UsedCounter");


        //plus = transform.FindChild<Button>("Cost/Plus");
        //minus = transform.FindChild<Button>("Cost/Minus");
        titleUI = transform.FindChild<TextMeshProUGUI>("Title");
        //costUI = transform.FindChild<TextMeshProUGUI>("Cost/Value");
        damageIcon = transform.FindChild<Transform>("SkillData/Damage/Icon");
        damageUI = transform.FindChild<TextMeshProUGUI>("SkillData/Damage/Value");
        descriptionUI = transform.FindChild<TextMeshProUGUI>("Description/Value");
        usedCounterIcon = transform.FindChild<Transform>("SkillData/UsedCounter/Icon");
        usedCounterUI = transform.FindChild<TextMeshProUGUI>("SkillData/UsedCounter/Value");
        //dangerLevelIncreaseUI = transform.FindChild<TextMeshProUGUI>("DangerLevelIncrease");
        bonusDamageUI = transform.FindChild<TextMeshProUGUI>("SkillData/UsedCounter/BonusDamageValue");

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Start() {
        //CheckButtonInteractability();

        //UpdateSlot();
    }

    private void Update() {
        //if (skill && skill.locked)
        //    unlockButton.Show();
        //else
        //    unlockButton.Hide();

        if (Input.GetMouseButton(0)) {
            if (InputHelper.instance.ClickedOnTag("InfoDamage")) {
                infoCost.gameObject.SetActive(false);
                infoDamage.gameObject.SetActive(true);
                infoUsedCounter.gameObject.SetActive(false);
            } else if (InputHelper.instance.ClickedOnTag("InfoCost")) {
                infoCost.gameObject.SetActive(true);
                infoDamage.gameObject.SetActive(false);
                infoUsedCounter.gameObject.SetActive(false);
            } else if (InputHelper.instance.ClickedOnTag("InfoUsedCounter")) {
                infoCost.gameObject.SetActive(false);
                infoDamage.gameObject.SetActive(false);
                infoUsedCounter.gameObject.SetActive(true);
            }
        } else {
            infoCost.gameObject.SetActive(false);
            infoDamage.gameObject.SetActive(false);
            infoUsedCounter.gameObject.SetActive(false);
        }
    }

    public void OnClickPlus() {
        skill.IncCost();
        UpdateSlot();
    }

    public void OnClickMinus() {
        skill.DecCost();
        UpdateSlot();
    }

    private void CheckButtonInteractability() {

        //if (minus == null || skill == null || plus == null)
        //    return;

        //minus.interactable = skill.cost > 1;
        //plus.interactable = skill.cost < Score.instance.skillPoints;
    }

    public void UnlockSkill() {
        //if (LevelData.Level >= skill.unlockLevel) {
        //skill.locked = false;
        //skill.barSlot.UpdateSlot();
        //purchaseSfx.Play();
        //unlockButton.Hide();
        UpdateSlot();
        //} else
        //    errorSfx.Play();
    }

    public void UpdateSlot() {

        //if (skill.locked) {
        //    plus.gameObject.SetActive(false);
        //    minus.gameObject.SetActive(false);
        //    costUI.gameObject.SetActive(false);
        //    titleUI.gameObject.SetActive(false);
        //    damageUI.gameObject.SetActive(false);
        //    damageIcon.gameObject.SetActive(false);
        //    usedCounterUI.gameObject.SetActive(false);
        //    bonusDamageUI.gameObject.SetActive(false);
        //    descriptionUI.gameObject.SetActive(false);
        //    usedCounterIcon.gameObject.SetActive(false);
        //    dangerLevelIncreaseUI.gameObject.SetActive(false);
        //} else {
        //plus.gameObject.SetActive(true);
        //minus.gameObject.SetActive(true);
        //costUI.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(true);
        damageUI.gameObject.SetActive(true);
        damageIcon.gameObject.SetActive(true);
        usedCounterUI.gameObject.SetActive(true);
        bonusDamageUI.gameObject.SetActive(true);
        descriptionUI.gameObject.SetActive(true);
        usedCounterIcon.gameObject.SetActive(true);
        //dangerLevelIncreaseUI.gameObject.SetActive(true);

        //skill.UpdateCost();
        //}

        image.sprite = skill.Icon;
        //costUI.text = skill.cost.ToString();
        damageUI.text = skill.GetTotalDamage(skill.cost).ToString();
        usedCounterUI.text = skill.BonusPercentage.ToString() + "%";
        bonusDamageUI.text = "(+" + skill.GetBonusDamage(skill.cost).ToString() + ")";
        //unlockButton?.SetText(skill.unlockLevel);
        //unlockButton?.SetColor(skill.unlockLevel);
        titleUI.text = skill.title;

        //if (skill.dangerLevelIncrease < 0)
        //    dangerLevelIncreaseUI.text = (skill.dangerLevelIncrease * skill.cost).ToString() + "%";
        //else
        //    dangerLevelIncreaseUI.text = "+" + (skill.dangerLevelIncrease * skill.cost).ToString() + "%";

        CheckButtonInteractability();
        skill.barSlot.UpdateSlot();
    }
}
