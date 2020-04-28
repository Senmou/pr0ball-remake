using UnityEngine;
using TMPro;

public class Item_Skillpoint : BaseItem {

    private TextMeshProUGUI valueUI;

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.ItemSkillPoint;

        valueUI = transform.FindChild<TextMeshProUGUI>("Value");

        int random = Random.Range(0, 100);
        if (random < 3)
            SetValue(Random.Range(5, 11), valueUI);
        else
            SetValue(Random.Range(1, 4), valueUI);
    }

    protected override void OnItemCollected() {
        Score.instance.IncSkillPoints(value);
    }
}
