public class Skill_B3 : Skill {

    public override void UseSkill() {
        Score.instance.IncScore(100000);
        Score.instance.IncReceivableGoldenPoints(100000);
    }
}
