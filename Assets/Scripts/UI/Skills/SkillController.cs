using UnityEngine;

public class SkillController : MonoBehaviour {

    public Skill[] selectedSkills;

    private void Awake() {
        selectedSkills = new Skill[4];
    }

    public void ChangeSkill(Skill oldSkill, Skill newSkill) {
        
        
    }
}
