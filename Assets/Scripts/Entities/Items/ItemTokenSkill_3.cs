public class ItemTokenSkill_3 : BaseItem {

    private Skill_Triggered skill_3;

    private void Start() {
        SetValue(1);
        entityType = CurrentLevelData.EntityType.ItemTokenSkill_3;
        skill_3 = FindObjectOfType<Skill_Triggered>();
    }

    protected override void OnItemCollected() {
        skill_3.hasToken = true;
        skillBar.UpdateSlots();
    }
}
