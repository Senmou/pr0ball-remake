public class ItemTokenSkill_2 : BaseItem {

    private Skill_Frogs skill_2;

    private void Start() {
        SetValue(1);
        entityType = CurrentLevelData.EntityType.ItemTokenSkill_2;
        skill_2 = FindObjectOfType<Skill_Frogs>();
    }

    protected override void OnItemCollected() {
        skill_2.tokenCount++;
        skillBar.UpdateSlots();
    }
}
