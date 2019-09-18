using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private TextMeshProUGUI priceLabel;
    private TextMeshProUGUI priceValue;
    private SkillMenuUnlockButton unlockButton;
    private WaveStateController waveStateController;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        image = GetComponent<Image>();
        skillMenu = FindObjectOfType<SkillMenu>();
        priceLabel = transform.FindChild<TextMeshProUGUI>("SkillData/Price/Label");
        priceValue = transform.FindChild<TextMeshProUGUI>("SkillData/Price/Value");
        waveStateController = FindObjectOfType<WaveStateController>();
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
        if (LevelData.Level >= skill.unlockLevel) {
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
        unlockButton?.SetText(skill.unlockLevel);
    }
}
