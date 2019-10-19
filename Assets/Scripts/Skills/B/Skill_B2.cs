public class Skill_B2 : Skill {

    public override void UseSkill() {
        Score.instance.IncScore(10000);
        Score.instance.IncReceivableGoldenPoints(10000);
    }
}
