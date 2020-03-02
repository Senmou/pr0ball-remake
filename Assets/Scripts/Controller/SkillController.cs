using UnityEngine;

public class SkillController : MonoBehaviour {

    [HideInInspector] public Skill[] skills;

    private void Awake() {
        skills = GetComponentsInChildren<Skill>();
    }

    public void ResetData() {
        foreach (var skill in skills) {
            skill.ResetData();
        }
    }
}
