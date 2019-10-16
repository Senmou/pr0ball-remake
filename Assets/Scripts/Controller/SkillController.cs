using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {
    public List<Skill> skills;

    public void ResetData() {
        foreach (var skill in skills) {
            skill.ResetData();
        }
    }
}
