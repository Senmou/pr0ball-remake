using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private TextMeshProUGUI upgradePriceUI;
    private SkillMenuUnlockButton unlockButton;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        image = GetComponent<Image>();
        skillMenu = FindObjectOfType<SkillMenu>();
        upgradePriceUI = transform.FindChild<TextMeshProUGUI>("SkillData/UpgradePrice/Value");
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
        } else
            errorSfx.Play();
    }

    public void UpgradeSkill() {
        skill.skillLevel++;
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        unlockButton?.SetText(skill.unlockLevel);
        upgradePriceUI.text = skill.UpgradePrice.ToString();
    }
}
