using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    [HideInInspector] public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private SkillMenuUnlockButton unlockButton;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI skillLevelUI;
    private TextMeshProUGUI skillpointsUI;
    private TextMeshProUGUI descriptionUI;

    private Transform infoLevel;
    private Transform infoDamage;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        image = transform.FindChild<Image>("Icon");
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        infoDamage = transform.FindChild<Transform>("InfoPopups/Damage");
        infoLevel = transform.FindChild<Transform>("InfoPopups/SkillLevel");
        damageUI = transform.FindChild<TextMeshProUGUI>("SkillData/Damage/Value");
        skillpointsUI = transform.FindChild<TextMeshProUGUI>("Skillpoints/Value");
        skillLevelUI = transform.FindChild<TextMeshProUGUI>("SkillData/SkillLevel/Value");
        descriptionUI = transform.FindChild<TextMeshProUGUI>("Description/Value");

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Update() {
        if (skill && skill.locked)
            unlockButton.Show();
        else
            unlockButton.Hide();

        if (Input.GetMouseButton(0)) {
            if (InputHelper.instance.ClickedOnTag("InfoLevel")) {
                infoLevel.gameObject.SetActive(true);
                infoDamage.gameObject.SetActive(false);
            }
            if (InputHelper.instance.ClickedOnTag("InfoDamage")) {
                infoLevel.gameObject.SetActive(false);
                infoDamage.gameObject.SetActive(true);
            }
        } else {
            infoLevel.gameObject.SetActive(false);
            infoDamage.gameObject.SetActive(false);
        }
    }

    public void UnlockSkill() {
        if (LevelData.Level >= skill.unlockLevel) {
            skill.locked = false;
            purchaseSfx.Play();
            unlockButton.Hide();
            UpdateSlot();
            skillMenu.lastSkillBarSlotClicked.EquipSkill(skill);
        } else
            errorSfx.Play();
    }

    public void UpgradeSkill() {
        if (Score.instance.PaySkillPoints(skill.UpgradePrice)) {
            skill.skillLevel++;
            purchaseSfx.Play();
        } else
            errorSfx.Play();
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        unlockButton?.SetText(skill.unlockLevel);
        damageUI.text = skill.Damage.ToString();
        skillLevelUI.text = skill.skillLevel.ToString();
        skillpointsUI.text = "Skillpunkte: " + Score.instance.skillPoints.ToString();
        descriptionUI.text = skill.description;
    }
}
