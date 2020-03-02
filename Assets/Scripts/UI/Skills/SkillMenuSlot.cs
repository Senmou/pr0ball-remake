﻿using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillMenuSlot : MonoBehaviour {

    [HideInInspector] public Skill skill;

    private Image image;
    private SkillMenu skillMenu;
    private SkillMenuUnlockButton unlockButton;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI descriptionUI;

    private Transform infoDamage;

    private AudioSource purchaseSfx;
    private AudioSource errorSfx;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
        image = transform.FindChild<Image>("Icon");
        unlockButton = GetComponentInChildren<SkillMenuUnlockButton>();

        infoDamage = transform.FindChild<Transform>("InfoPopups/Damage");
        damageUI = transform.FindChild<TextMeshProUGUI>("SkillData/Damage/Value");
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
            if (InputHelper.instance.ClickedOnTag("InfoDamage")) {
                infoDamage.gameObject.SetActive(true);
            }
        } else {
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

    public void UpdateSlot() {
        image.sprite = skill.Icon;
        unlockButton?.SetText(skill.unlockLevel);
        unlockButton?.SetColor(skill.unlockLevel);
        damageUI.text = skill.Damage.ToString();
        descriptionUI.text = (skill.locked) ? "" : skill.description;
    }
}
