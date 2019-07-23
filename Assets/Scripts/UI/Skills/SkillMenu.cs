using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public int skillMenuId;
    public SkillBase[] skills;

    private SkillBar skillBar;
    private SkillManager skillManager;

    private void Awake() {
        skillBar = FindObjectOfType<SkillBar>();
        skillManager = FindObjectOfType<SkillManager>();
    }

    public void OnSkillSelected(int skillId) {
        skillBar.SetSkill(skillMenuId, skillManager.GetSkill(skillId));
    }
}
