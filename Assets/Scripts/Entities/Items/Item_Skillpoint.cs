using UnityEngine;
using TMPro;

public class Item_Skillpoint : BaseItem {

    private TextMeshProUGUI valueUI;

    private new void Awake() {
        base.Awake();
        entityType = CurrentLevelData.EntityType.ItemSkillPoint;

        valueUI = transform.FindChild<TextMeshProUGUI>("Value");

        RemoteConfig.RemoteItemSkillPointValues remoteValues = RemoteConfig.instance.remoteItemSkillPointValues;

        int random = Random.Range(0, 100);
        if (random < 3)
            SetValue(Random.Range(remoteValues.minValue * 5, remoteValues.maxValue * 4), valueUI);
        else
            SetValue(Random.Range(remoteValues.minValue, remoteValues.maxValue + 1), valueUI);
    }

    protected override void OnItemCollected() {
        Score.instance.IncSkillPoints(value);
    }
}
