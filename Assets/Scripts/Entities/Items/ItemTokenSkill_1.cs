public class ItemTokenSkill_1 : BaseItem {

    private Skill_Hammertime skill_1;

    private void Start() {
        SetValue(1);
        entityType = CurrentLevelData.EntityType.ItemTokenSkill_1;
        skill_1 = FindObjectOfType<Skill_Hammertime>();
    }

    protected override void OnItemCollected() {
        skill_1.tokenCount++;
        skillBar.UpdateSlots();
    }
}
