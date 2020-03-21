using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Benitrator : MonoBehaviour {

    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource coins;
    [SerializeField] private AudioSource winBig;
    [SerializeField] private AudioSource winSmall;
    [SerializeField] private AudioSource wheelStop;

    private Wheel[] wheels;

    private int bet;
    private bool isNewRoundStarted;
    private bool IsRotating { get => wheels[0].isRotating || wheels[1].isRotating || wheels[2].isRotating; }

    public delegate void ResultDelegate(SlotType resultSlotType, string name);
    private ResultDelegate onStoppedRotating;

    private TextMeshProUGUI betUI;
    private TextMeshProUGUI winUI;
    private TextMeshProUGUI benisUI;
    private TextMeshProUGUI resultUI;
    private TextMeshProUGUI blussiUI;

    private BallMenu ballMenu;
    private Transform proChan;
    private AudioSource audioSource;
    private Image startButtonBackground;
    private Color winTextColorDefault;

    private Dictionary<SlotType, int> totalResults = new Dictionary<SlotType, int>();

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        wheels = GetComponentsInChildren<Wheel>();
        audioSource = GetComponent<AudioSource>();
        proChan = transform.FindChild<Transform>("proChan");
        betUI = transform.FindChild<TextMeshProUGUI>("Bet/Value");
        winUI = transform.FindChild<TextMeshProUGUI>("Win/Value");
        benisUI = transform.FindChild<TextMeshProUGUI>("Benis/Value");
        resultUI = transform.FindChild<TextMeshProUGUI>("Result/Text");
        blussiUI = transform.FindChild<TextMeshProUGUI>("Blussis/Value");
        startButtonBackground = transform.FindChild<Image>("Start_Button/Background");
        resultUI.gameObject.SetActive(false);
        onStoppedRotating += OnStoppedRotating;

        winUI.text = "";
        winTextColorDefault = winUI.color;

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

        if (CanvasManager.instance.CurrentActiveCanvasType != CanvasType.BALLS)
            return;

        if (Score.instance.skillPoints == 0)
            startButtonBackground.color = new Color(0.35f, 0.35f, 0.35f, 1f); // grey
        else
            startButtonBackground.color = new Color(0.8235295f, 0.2352941f, 0.1333333f, 1f); // red

        benisUI.text = Score.instance.score.ToString();
        blussiUI.text = Score.instance.skillPoints.ToString();
    }

    public void ShowProChanIfZeroSkillPoints() {
        if (Score.instance.skillPoints == 0)
            proChan.gameObject.SetActive(true);
        else
            proChan.gameObject.SetActive(false);
    }

    public void SetInitialBetIfEnoughSkillPoints() {
        if (Score.instance.skillPoints >= 1) {
            bet = 1;
            UpdateUI();
        }
    }

    public void StartBenitrator() {

        if (bet == 0) {
            ErrorMessage.instance.Show(1f, "Dein Einsatz bitte!");
            return;
        }

        if (isNewRoundStarted) {
            ErrorMessage.instance.Show(1f, "Die Runde läuft bereits!");
            return;
        }

        if (Score.instance.PaySkillPoints(bet)) {
            PlayWheelStopSfx();
            isNewRoundStarted = true;
            SetWinText("");
            Statistics.Instance.benitrator.plays++;
            Statistics.Instance.benitrator.totalBets += bet;
            StartCoroutine(RotateWheels());
        } else {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Nicht genug Blussis!");
        }
    }

    public void IncBet() {

        if (IsRotating) {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Jetzt nicht!");
            return;
        }

        if (bet == 3) {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Three, take it or leave it!");
            return;
        }

        if (bet < 3 && Score.instance.skillPoints >= bet + 1) {
            bet++;
            PlayCoinSfx();
            UpdateUI();
        } else {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Nicht genug Blussis!");
        }
    }

    public void DecBet() {

        if (IsRotating) {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Jetzt nicht!");
            return;
        }

        if (bet > 0) {
            bet--;
            PlayCoinSfx();
            UpdateUI();
        } else {
            ballMenu.PlayErrorSound();
            ErrorMessage.instance.Show(1f, "Das geht nicht! B-Baka!");
        }
    }

    private void UpdateUI() {
        betUI.text = bet.ToString();
    }

    private void EvaluateResult() {

        // Skill points
        int skillPointSymbolCount = -1;
        totalResults.TryGetValue(SlotType.SkillPoint, out skillPointSymbolCount);
        if (skillPointSymbolCount > 1) {
            Score.instance.IncSkillPoints(GetRewardSkillPoints(skillPointSymbolCount));
            SetWinText(skillPointSymbolCount.ToString() + " Blussis");
        }

        // Damage
        int damageSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Damage, out damageSymbolCount);
        if (damageSymbolCount > 1) {
            int rewardDamage = GetRewardDamage(damageSymbolCount);
            BallStats.Instance.damage += rewardDamage;
            SetWinText(rewardDamage.ToString() + " Schaden");
        }

        // Crit chance
        int critChanceSymbolCount = -1;
        totalResults.TryGetValue(SlotType.CritChance, out critChanceSymbolCount);
        if (critChanceSymbolCount > 1) {
            float rewardCritChance = GetRewardCritChance(critChanceSymbolCount);
            BallStats.Instance.IncCritChance(rewardCritChance);
            SetWinText(rewardCritChance.ToString() + "% Kritische Trefferchance");
        }

        // Crit damage
        int critDamageSymbolCount = -1;
        totalResults.TryGetValue(SlotType.CritDamage, out critDamageSymbolCount);
        if (critDamageSymbolCount > 1) {
            float rewardCritDamage = GetRewardCritDamage(critDamageSymbolCount);
            BallStats.Instance.critDamage += rewardCritDamage;
            SetWinText(rewardCritDamage.ToString() + "x Kritischer Schaden");
        }

        // Balls
        int ballSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Ball, out ballSymbolCount);
        if (ballSymbolCount > 1) {
            int rewardBalls = GetRewardBalls(ballSymbolCount);
            BallStats.Instance.AddBalls(rewardBalls);

            if (rewardBalls == 1)
                SetWinText(rewardBalls.ToString() + " Ball");
            else
                SetWinText(rewardBalls.ToString() + " Bälle");
        }

        // Score
        int scoreSymbolCount = -1;
        totalResults.TryGetValue(SlotType.Score, out scoreSymbolCount);
        if (scoreSymbolCount > 1) {
            int rewardScore = GetRewardScore(scoreSymbolCount);
            Score.instance.IncScore(rewardScore);
            SetWinText(rewardScore.ToString() + " Benis");
        }

        // Three identical symbols
        if (skillPointSymbolCount > 2 || damageSymbolCount > 2 || critChanceSymbolCount > 2 ||
            critDamageSymbolCount > 2 || ballSymbolCount > 2 || scoreSymbolCount > 2) {
            MusicController.instance.ChangeVolumeForSeconds(0.2f, 1f);
            winBig.Play();
            Statistics.Instance.benitrator.wins++;
            StartCoroutine(ShowResultText("GEWONNEN"));
            // Two identical symbols
        } else if (skillPointSymbolCount > 1 || damageSymbolCount > 1 || critChanceSymbolCount > 1 ||
           critDamageSymbolCount > 1 || ballSymbolCount > 1 || scoreSymbolCount > 1) {
            MusicController.instance.ChangeVolumeForSeconds(0.2f, 1f);
            winSmall.Play();
            Statistics.Instance.benitrator.wins++;
            StartCoroutine(ShowResultText("GEWONNEN"));
        } else {
            ballMenu.PlayErrorSound();
            int dangerLevelIncrease = 2 + bet;
            LevelData.DangerLevel += dangerLevelIncrease;
            SetWinText("+" + dangerLevelIncrease + "% GEFAHR", useColorRed: true);
            Statistics.Instance.benitrator.loses++;
            StartCoroutine(ShowResultText("VERLOREN"));
        }

        ballMenu.UpdateUI();
        Score.instance.UpdateUI();
        totalResults.Clear();
    }

    private void SetWinText(string text, bool useColorRed = false) {
        winUI.text = text;
        winUI.color = useColorRed ? Color.red : winTextColorDefault;
    }

    private IEnumerator ShowResultText(string text) {
        resultUI.gameObject.SetActive(true);
        resultUI.text = text;
        yield return new WaitForSecondsRealtime(3f);
        resultUI.gameObject.SetActive(false);
    }

    private IEnumerator ShowDangerLevelIncrease(int value) {
        resultUI.gameObject.SetActive(true);
        string sign = (value < 0) ? "-" : "+";
        resultUI.text = sign + value.ToString();
        yield return new WaitForSecondsRealtime(3f);
        resultUI.gameObject.SetActive(false);
    }

    private int GetRewardSkillPoints(int symbolCount) {
        int rewardSkillPoints = 0;

        if (bet == 1)
            rewardSkillPoints = 2;
        else if (bet == 2)
            rewardSkillPoints = 5;
        else if (bet == 3)
            rewardSkillPoints = 10;

        if (symbolCount == 3) {
            rewardSkillPoints += 2 * bet;
        }

        return rewardSkillPoints;
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

        return rewardCritChance;
    }

    private float GetRewardCritDamage(int symbolCount) {
        float rewardCritDamage = 0;

        if (bet == 1)
            rewardCritDamage = 0.1f;
        else if (bet == 2)
            rewardCritDamage = 0.25f;
        else if (bet == 3)
            rewardCritDamage = 1f;

        if (symbolCount == 3) {
            rewardCritDamage += 0.25f * bet;
        }

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
            rewardBalls += 1;
        }

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

        long currentScore = Score.instance.score;
        int rewardScore = (int)((currentScore + 250) * rewardScoreMultiplier);

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

    public void PlayClickSfx() => click.Play();
    public void PlayCoinSfx() => coins.Play();
    public void PlayWheelStopSfx() => wheelStop.Play();
}
