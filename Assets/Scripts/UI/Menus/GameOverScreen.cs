using UnityEngine;
using System;
using TMPro;

public class GameOverScreen : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI playtimeUI;
    private TextMeshProUGUI highscoreUI;
    private GameController gameController;
    private PauseBackground pauseBackground;

    // Statistics

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
        moveUI = GetComponent<MoveUI>();
        gameController = FindObjectOfType<GameController>();
        playtimeUI = transform.FindChild<TextMeshProUGUI>("Highscore/Playtime");
        highscoreUI = transform.FindChild<TextMeshProUGUI>("Highscore/Value");

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

        pauseBackground = FindObjectOfType<PauseBackground>();
    }

    private void UpdateStatisticsUI() {

        // Balls
        ballsCollisionsUI.text = Statistics.Instance.balls.collisions.ToString();
        ballsTotalDamageUI.text = Statistics.Instance.balls.damageDealt.ToString();
        ballsFiredUI.text = Statistics.Instance.balls.fired.ToString();

        // Benitrat0r
        totalBetsUI.text = Statistics.Instance.benitrator.totalBets.ToString();
        averageBetUI.text =  Statistics.Instance.benitrator.AverageBet.ToString("0.##");
        winsUI.text = Statistics.Instance.benitrator.wins.ToString();
        losesUI.text = Statistics.Instance.benitrator.loses.ToString();
        winsPercentageUI.text = Statistics.Instance.benitrator.WinsPercentage.ToString("0%");
        losesPercentageUI.text = Statistics.Instance.benitrator.LosesPercentage.ToString("0%");

        // Enemies
        enemiesKilledUI.text = Statistics.Instance.enemies.killed.ToString();

        // Skills
        skillsUsedTotalUI.text = Statistics.Instance.skills.SkillsUsedTotal.ToString();
        hammertimeUsedUI.text = Statistics.Instance.skills.skill_1.used.ToString();
        frogUsedUI.text = Statistics.Instance.skills.skill_2.used.ToString();
        triggerUsedUI.text = Statistics.Instance.skills.skill_3.used.ToString();
        skillsDamageTotalUI.text = Statistics.Instance.skills.SkillsDamageDealtTotal.ToString();
        hammertimeDamageUI.text = Statistics.Instance.skills.skill_1.damageDealt.ToString();
        frogsDamageUI.text = Statistics.Instance.skills.skill_2.damageDealt.ToString();
        triggerDamageUI.text = Statistics.Instance.skills.skill_3.damageDealt.ToString();
        hammertimeDamagePercentageUI.text = Statistics.Instance.DamageDealtPercentage_Skill_1.ToString("0%");
        frogsDamagePercentageUI.text = Statistics.Instance.DamageDealtPercentage_Skill_2.ToString("0%");
        triggerDamagePercentageUI.text = Statistics.Instance.DamageDealtPercentage_Skill_3.ToString("0%");
    }

    public override void Show() {
        SaveHighscore();
        UpdateStatisticsUI();
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, 55f), 0.5f, true);
    }

    private void SaveHighscore() {
        string timestamp = "Gewachsen seit " + DateTime.Now.ToString("dd. MMMM yyyy") + " (" + gameController.GetPlaytimeMinutes() + " Minuten)";
        playtimeUI.text = timestamp;
        highscoreUI.text = Score.instance.highscore.ToString();
        PersistentData.instance.highscores.AddHighscore(Score.instance.highscore, timestamp);
    }
}
