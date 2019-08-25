using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuSlot : MonoBehaviour {

    public Skill skill;

    private Image image;
    private TextMeshProUGUI price;
    private SkillMenu skillMenu;
    private SkillMenuUnlockButton unlockButton;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        image = GetComponent<Image>();
        price = GetComponentInChildren<TextMeshProUGUI>();
        skillMenu = FindObjectOfType<SkillMenu>();
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Update() {
        if (skill.locked) {
            unlockButton.Show();
        }
    }

    public void UnlockSkill() {
        if (Benis.instance.BuySkill(skill)) {
            skill.locked = false;
            purchaseSfx.Play();
            unlockButton.Hide();
        } else
            errorSfx.Play();
    }

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        price.text = skill.price.ToString();
        unlockButton?.SetText(skill.price);
    }
}
