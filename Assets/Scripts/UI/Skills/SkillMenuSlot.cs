using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    [HideInInspector] public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private SkillMenuUnlockButton unlockButton;

    private Button plus;
    private Button minus;
    private TextMeshProUGUI costUI;
    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI descriptionUI;
    private TextMeshProUGUI usedCounterUI;
    private TextMeshProUGUI bonusDamageUI;

    private Transform infoCost;
    private Transform infoDamage;
    private Transform infoUsedCounter;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        image = transform.FindChild<Image>("Icon");
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        infoCost = transform.FindChild<Transform>("InfoPopups/Cost");
        infoDamage = transform.FindChild<Transform>("InfoPopups/Damage");
        infoUsedCounter = transform.FindChild<Transform>("InfoPopups/UsedCounter");

        plus = transform.FindChild<Button>("Cost/Plus");
        minus = transform.FindChild<Button>("Cost/Minus");
        costUI = transform.FindChild<TextMeshProUGUI>("Cost/Value");
        damageUI = transform.FindChild<TextMeshProUGUI>("SkillData/Damage/Value");
        descriptionUI = transform.FindChild<TextMeshProUGUI>("Description/Value");
        usedCounterUI = transform.FindChild<TextMeshProUGUI>("SkillData/UsedCounter/Value");
        bonusDamageUI = transform.FindChild<TextMeshProUGUI>("SkillData/UsedCounter/BonusDamageValue");

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Start() {
        CheckButtonInteractability();
    }

    private void Update() {
        if (skill && skill.locked)
            unlockButton.Show();
        else
            unlockButton.Hide();

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

        if (minus == null || skill == null || plus == null)
            return;

        minus.interactable = skill.cost > 1;
        plus.interactable = skill.cost < Score.instance.skillPoints;
    }

    public void UnlockSkill() {
        if (LevelData.Level >= skill.unlockLevel) {
            skill.locked = false;
            skill.barSlot.UpdateSlot(skill);
            purchaseSfx.Play();
            unlockButton.Hide();
            UpdateSlot();
        } else
            errorSfx.Play();
    }

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        costUI.text = skill.cost.ToString();
        damageUI.text = skill.TotalDamage.ToString();
        usedCounterUI.text = skill.BonusPercentage.ToString() + "%";
        bonusDamageUI.text = "(+" + skill.BonusDamage.ToString() + ")";
        unlockButton?.SetText(skill.unlockLevel);
        unlockButton?.SetColor(skill.unlockLevel);
        descriptionUI.text = (skill.locked) ? "" : skill.description;

        CheckButtonInteractability();
        skill.barSlot.UpdateCostUI(skill.cost);
    }
}
