using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class Benitrator : MonoBehaviour {

    private Wheel[] wheels;

    private int bet;
    private bool isNewRoundStarted;
    private bool IsRotating { get => wheels[0].isRotating || wheels[1].isRotating || wheels[2].isRotating; }

    public delegate void ResultDelegate(SlotType resultSlotType, string name);
    private ResultDelegate onStoppedRotating;

    private BallMenu ballMenu;
    private TextMeshProUGUI betUI;
    private TextMeshProUGUI resultUI;
    private Dictionary<SlotType, int> totalResults = new Dictionary<SlotType, int>();

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        wheels = GetComponentsInChildren<Wheel>();
        betUI = transform.FindChild<TextMeshProUGUI>("Bet");
        resultUI = transform.FindChild<TextMeshProUGUI>("Result");
        resultUI.text = "";
        onStoppedRotating += OnStoppedRotating;

        UpdateUI();
    }

    private void Update() {

        if (isNewRoundStarted && !IsRotating) {
            EvaluateResult();
            isNewRoundStarted = false;

            if (bet > Score.instance.skillPoints) {
                bet = Score.instance.skillPoints;
                UpdateUI();
            }
        }
    }

    public void StartBenitrator() {
        if (!isNewRoundStarted && bet > 0 && Score.instance.PaySkillPoints(bet)) {
            ballMenu.PlaySuccessSound();
            isNewRoundStarted = true;
            StartCoroutine(RotateWheels());
        } else
            ballMenu.PlayErrorSound();
    }

    public void IncBet() {

        if (bet < 3 && Score.instance.skillPoints >= bet + 1) {
            bet++;
            UpdateUI();
        } else
            ballMenu.PlayErrorSound();
    }

    public void DecBet() {

        if (bet > 0) {
            bet--;
            UpdateUI();
        } else
            ballMenu.PlayErrorSound();
    }

    private void UpdateUI() {
        betUI.text = bet.ToString();
    }

    private void EvaluateResult() {

        // Gain one skill point for every skill point symbol
        int rewardedSkillPointCount = -1;
        totalResults.TryGetValue(SlotType.SkillPoint, out rewardedSkillPointCount);
        if (rewardedSkillPointCount > 1)
            Score.instance.IncSkillPoints(rewardedSkillPointCount);

        // Damage
        int damageSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Damage, out damageSymbolCount);
        if (damageSymbolCount > 1)
            BallStats.Instance.damage += GetRewardDamage(damageSymbolCount);

        // Crit chance
        int critChanceSymbolCount = -1;
        totalResults.TryGetValue(SlotType.CritChance, out critChanceSymbolCount);
        if (critChanceSymbolCount > 1)
            BallStats.Instance.critChance += GetRewardCritChance(critChanceSymbolCount);

        // Crit damage
        int critDamageSymbolCount = -1;
        totalResults.TryGetValue(SlotType.CritDamage, out critDamageSymbolCount);
        if (critDamageSymbolCount > 1)
            BallStats.Instance.critDamage += GetRewardDamage(critDamageSymbolCount);

        // Balls
        int ballSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Ball, out ballSymbolCount);
        if (ballSymbolCount > 1)
            BallStats.Instance.AddBalls(GetRewardBalls(ballSymbolCount));

        // Score
        int scoreSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Score, out scoreSymbolCount);
        if (scoreSymbolCount > 1)
            Score.instance.IncScore(GetRewardScore(scoreSymbolCount));

        if (rewardedSkillPointCount > 1 || damageSymbolCount > 1 || critChanceSymbolCount > 1 ||
            critDamageSymbolCount > 1 || ballSymbolCount > 1 || scoreSymbolCount > 1)
            resultUI.text = "GEWONNEN";
        else
            resultUI.text = "VERLOREN";

        ballMenu.UpdateUI();
        Score.instance.UpdateUI();
        totalResults.Clear();
    }

    private int GetRewardDamage(int symbolCount) {
        int rewardDamage = 0;

        if (bet == 1)
            rewardDamage = 1;
        else if (bet == 2)
            rewardDamage = 2;
        else if (bet == 3)
            rewardDamage = 3;

        if (symbolCount == 3) {
            rewardDamage += bet;
        }
        Debug.Log("You won: " + rewardDamage + " Damage");
        return rewardDamage;
    }

    private int GetRewardCritChance(int symbolCount) {
        int rewardCritChance = 0;

        if (bet == 1)
            rewardCritChance = 2;
        else if (bet == 2)
            rewardCritChance = 5;
        else if (bet == 3)
            rewardCritChance = 10;

        if (symbolCount == 3) {
            rewardCritChance += 2 * bet;
        }

        Debug.Log("You won: " + rewardCritChance + " CritChance");
        return rewardCritChance;
    }

    private float GetRewardCritDamage(int symbolCount) {
        float rewardCritDamage = 0;

        if (bet == 1)
            rewardCritDamage = 0.1f;
        else if (bet == 2)
            rewardCritDamage = 0.3f;
        else if (bet == 3)
            rewardCritDamage = 0.5f;

        if (symbolCount == 3) {
            rewardCritDamage += 0.25f * bet;
        }

        Debug.Log("You won: " + rewardCritDamage + " CritDamage");
        return rewardCritDamage;
    }

    private int GetRewardBalls(int symbolCount) {
        int rewardBalls = 0;

        if (bet == 1)
            rewardBalls = 1;
        else if (bet == 2)
            rewardBalls = 2;
        else if (bet == 3)
            rewardBalls = 3;

        if (symbolCount == 3) {
            rewardBalls += 2 * bet;
        }

        Debug.Log("You won: " + rewardBalls + " Balls");
        return rewardBalls;
    }

    private int GetRewardScore(int symbolCount) {
        float rewardScoreMultiplier = 0;

        if (bet == 1)
            rewardScoreMultiplier = 0.1f;
        else if (bet == 2)
            rewardScoreMultiplier = 0.3f;
        else if (bet == 3)
            rewardScoreMultiplier = 0.5f;

        if (symbolCount == 3) {
            rewardScoreMultiplier *= bet;
        }

        int currentScore = Score.instance.score;
        int rewardScore = (int)((currentScore + 250) * rewardScoreMultiplier);

        Debug.Log("You won: " + rewardScore + " Benis");
        return rewardScore;
    }

    private IEnumerator RotateWheels() {

        foreach (var wheel in wheels) {
            wheel.StartRotating(onStoppedRotating);
        }
        yield return null;
    }

    private void OnStoppedRotating(SlotType resultSlotType, string name) {

        if (totalResults.ContainsKey(resultSlotType))
            totalResults[resultSlotType]++;
        else
            totalResults.Add(resultSlotType, 1);
    }
}
