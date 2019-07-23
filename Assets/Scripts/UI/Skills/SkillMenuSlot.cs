using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuSlot : MonoBehaviour {

    public Skill skill;

    private Image image;
    private TextMeshProUGUI skillName;
    private SkillMenu skillMenu;

    private void Awake() {
        image = GetComponent<Image>();
        skillName = GetComponentInChildren<TextMeshProUGUI>();
        skillMenu = FindObjectOfType<SkillMenu>();
    }

    public void UpdateSlot() {
        image.sprite = skill.icon;
        skillName.text = skill.name;
    }
}
