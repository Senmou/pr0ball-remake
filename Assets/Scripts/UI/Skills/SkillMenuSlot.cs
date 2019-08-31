using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuSlot : MonoBehaviour {

    public Skill skill;

    private Image image;
    private TextMeshProUGUI priceLabel;
    private TextMeshProUGUI priceValue;
    private SkillMenu skillMenu;
    private SkillMenuUnlockButton unlockButton;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        image = GetComponent<Image>();
        priceLabel = transform.FindChild<TextMeshProUGUI>("SkillData/Price/Label");
        priceValue = transform.FindChild<TextMeshProUGUI>("SkillData/Price/Value");
        skillMenu = FindObjectOfType<SkillMenu>();
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
    }

    private void Start() {
        priceLabel.text = "Preis:";
    }

    private void Update() {
        if (skill.locked)
            unlockButton.Show();
        else
            unlockButton.Hide();
    }

    public void UnlockSkill() {
        if (Benis.instance.BuySkill(skill)) {
            skill.locked = false;
            purchaseSfx.Play();
            unlockButton.Hide();
            UpdateSlot();
        } else
            errorSfx.Play();
    }

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        priceValue.text = skill.price.ToString();
        unlockButton?.SetText(skill.price);
    }
}
