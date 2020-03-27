using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillBarSlot : MonoBehaviour {

    public Skill skill;
    public Color32 disabledColor;
    public Sprite defaultSprite;

    private Image image;
    //private Image clockImage;
    //private TextMeshProUGUI costUI;
    private TextMeshProUGUI tokenUI;
    private SkillController skillController;

    private void Awake() {
        image = GetComponent<Image>();
        //clockImage = transform.FindChild<Image>("Clock");
        //costUI = transform.FindChild<TextMeshProUGUI>("Cost");
        tokenUI = transform.FindChild<TextMeshProUGUI>("Token/Value");
        skillController = FindObjectOfType<SkillController>();
        //clockImage.gameObject.SetActive(false);
        //costUI.gameObject.SetActive(false);
    }

    public void UseSkill() {
        if (!skill.locked)
            skill.UseSkill();
    }

    public void ShowClockImage(bool state) {
        //clockImage.gameObject.SetActive(state);
    }

    public void UpdateSlot() {

        //if (skill == null) {
        //    image.sprite = defaultSprite;
        //    return;
        //}

        //if (!skill.locked) {
        //costUI.text = skill.cost.ToString();
        //costUI.gameObject.SetActive(true);
        //}

        //if (skill.locked)
        //image.sprite = defaultSprite;
        //else

        tokenUI.text = skill.tokenCount + "/" + skill.tokenCost;

        if (skill.tokenCount < skill.tokenCost)
            image.color = disabledColor;
        else
            image.color = new Color(1f, 1f, 1f, 1f);

        image.sprite = skill.Icon;


        //ShowClockImage(skill.usedThisTurn);
        //costUI.gameObject.SetActive(!skill.locked);
    }
}
