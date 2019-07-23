using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public List<SkillBase> allSkills;

    private SkillBar skillBar;

    private void Awake() {
        skillBar = FindObjectOfType<SkillBar>();
    }

    public void UnlockSkill(SkillBase skillToUnlock) {
        skillToUnlock.isUnlocked = true;
    }

    public void AddSkillToSkillBar(SkillBase skill, int slotID) {
        skillBar.equipedSkills[slotID] = skill;
    }

    public SkillBase GetSkill(int skillId) => allSkills[skillId];
}
