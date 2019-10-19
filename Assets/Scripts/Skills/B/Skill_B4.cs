public class Skill_B4 : Skill {

    public override void UseSkill() {
        Score.instance.IncScore(100000000);
        Score.instance.IncReceivableGoldenPoints(100000000);
    }
}
