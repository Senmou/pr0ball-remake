using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    [HideInInspector] public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private TextMeshProUGUI upgradePriceUI;
    private SkillMenuUnlockButton unlockButton;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        image = transform.FindChild<Image>("Icon");
        upgradePriceUI = transform.FindChild<TextMeshProUGUI>("UpgradeButton/Price");
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Update() {
        if (skill && skill.locked)
            unlockButton.Show();
        else
            unlockButton.Hide();
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
        upgradePriceUI.text = skill.UpgradePrice.ToString();
    }
}
