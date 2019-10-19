public class Skill_B1 : Skill {

    public override void UseSkill() {
        Score.instance.IncScore(1000);
        Score.instance.IncReceivableGoldenPoints(1000);
    }
}
