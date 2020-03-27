using UnityEngine;
using TMPro;

public class StatisticsMenu : CanvasController {

    private Statistics statistics;

    private MoveUI moveUI;
    private PauseBackground pauseBackground;

    // Balls
    private TextMeshProUGUI ballsCollisionsUI;
    private TextMeshProUGUI ballsTotalDamageUI;
    private TextMeshProUGUI ballsFiredUI;

    // Benitrat0r
    private TextMeshProUGUI totalBetsUI;
    private TextMeshProUGUI averageBetUI;
    private TextMeshProUGUI winsUI;
    private TextMeshProUGUI losesUI;
    private TextMeshProUGUI winsPercentageUI;
    private TextMeshProUGUI losesPercentageUI;

    // Enemies
    private TextMeshProUGUI enemiesKilledUI;

    // Skills
    private TextMeshProUGUI skillsUsedTotalUI;
    private TextMeshProUGUI hammertimeUsedUI;
    private TextMeshProUGUI frogUsedUI;
    private TextMeshProUGUI triggerUsedUI;
    private TextMeshProUGUI skillsDamageTotalUI;
    private TextMeshProUGUI hammertimeDamageUI;
    private TextMeshProUGUI frogsDamageUI;
    private TextMeshProUGUI triggerDamageUI;
    private TextMeshProUGUI hammertimeDamagePercentageUI;
    private TextMeshProUGUI frogsDamagePercentageUI;
    private TextMeshProUGUI triggerDamagePercentageUI;

    private void Awake() {

        statistics = null;
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();

        // Balls
        ballsCollisionsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/Collisions/Value");
        ballsTotalDamageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/TotalDamage/Value");
        ballsFiredUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BallStatistics/Fired/Value");

        // Benitrat0r
        totalBetsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/TotalBets/Value");
        averageBetUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/AverageBet/Value");
        winsUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/Wins/Value");
        losesUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/Loses/Value");
        winsPercentageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/Wins/ValuePercentage");
        losesPercentageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/BenitratorStatistics/Loses/ValuePercentage");

        // Enemies
        enemiesKilledUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/EnemyStatistics/Kills/Value");

        // Skills
        skillsUsedTotalUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Used/UsedTotal/Value");
        hammertimeUsedUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Used/Used_Skill_1/Value");
        frogUsedUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Used/Used_Skill_2/Value");
        triggerUsedUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Used/Used_Skill_3/Value");
        skillsDamageTotalUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/DamageTotal/Value");
        hammertimeDamageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_1/Value");
        frogsDamageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_2/Value");
        triggerDamageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_3/Value");
        hammertimeDamagePercentageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_1/ValuePercentage");
        frogsDamagePercentageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_2/ValuePercentage");
        triggerDamagePercentageUI = transform.FindChild<TextMeshProUGUI>("Statistics/Table/SkillStatistics/Damage/Damage_Skill_3/ValuePercentage");

    }

    public void UpdateStatisticsUI(Statistics statistics) {

        // Balls
        ballsCollisionsUI.text = statistics.balls.collisions.ToString();
        ballsTotalDamageUI.text = statistics.balls.damageDealt.ToString();
        ballsFiredUI.text = statistics.balls.fired.ToString();

        // Benitrat0r
        totalBetsUI.text = statistics.benitrator.totalBets.ToString();
        averageBetUI.text = statistics.benitrator.AverageBet.ToString("0.##");
        winsUI.text = statistics.benitrator.wins.ToString();
        losesUI.text = statistics.benitrator.loses.ToString();
        winsPercentageUI.text = statistics.benitrator.WinsPercentage.ToString("0%");
        losesPercentageUI.text = statistics.benitrator.LosesPercentage.ToString("0%");

        // Enemies
        enemiesKilledUI.text = statistics.enemies.killed.ToString();

        // Skills
        skillsUsedTotalUI.text = statistics.skills.SkillsUsedTotal.ToString();
        hammertimeUsedUI.text = statistics.skills.skill_1.used.ToString();
        frogUsedUI.text = statistics.skills.skill_2.used.ToString();
        triggerUsedUI.text = statistics.skills.skill_3.used.ToString();
        skillsDamageTotalUI.text = statistics.skills.SkillsDamageDealtTotal.ToString();
        hammertimeDamageUI.text = statistics.skills.skill_1.damageDealt.ToString();
        frogsDamageUI.text = statistics.skills.skill_2.damageDealt.ToString();
        triggerDamageUI.text = statistics.skills.skill_3.damageDealt.ToString();
        hammertimeDamagePercentageUI.text = statistics.DamageDealtPercentage_Skill_1.ToString("0%");
        frogsDamagePercentageUI.text = statistics.DamageDealtPercentage_Skill_2.ToString("0%");
        triggerDamagePercentageUI.text = statistics.DamageDealtPercentage_Skill_3.ToString("0%");
    }

    public void SetStatistics(Statistics _statistics) {
        statistics = _statistics;
        UpdateStatisticsUI(statistics);
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f, true);
    }
}
