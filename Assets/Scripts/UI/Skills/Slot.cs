using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public Skill skill;
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() {
        UpdateSlot();
    }

    public void UpdateSlot() {
        image.color = skill.color;
    }

    public void PrintSkillName() {
        Debug.Log(skill.skillName);
    }

    public void SwapSkills(Slot skillBarSlot) {
        Skill skillToSwap = skillBarSlot.skill;
        skillBarSlot.skill = skill;
        skill = skillToSwap;
        skillBarSlot.UpdateSlot();
        UpdateSlot();
    }
}
