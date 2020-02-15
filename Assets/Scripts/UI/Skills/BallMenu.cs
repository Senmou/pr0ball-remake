﻿using UnityEngine;
using TMPro;

public class BallMenu : MonoBehaviour {

    private TextMeshProUGUI upgradePriceUI;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI critChanceUI;
    private TextMeshProUGUI critDamageUI;
    private TextMeshProUGUI ballCountUI;

    private TextMeshProUGUI upgradeDamageUI;
    private TextMeshProUGUI upgradeCritChanceUI;
    private TextMeshProUGUI upgradeCritDamageUI;
    private TextMeshProUGUI upgradeBallCountUI;

    private Transform infoDamage;
    private Transform infoCritChance;
    private Transform infoCritDamage;
    private Transform infoBallCount;

    private MoveUI moveUI;
    private BallController ballController;
    private GameStateController gameStateController;

    private TextMeshProUGUI scoreUI;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();

        scoreUI = transform.FindChild<TextMeshProUGUI>("Score/Value");

        upgradePriceUI = transform.FindChild<TextMeshProUGUI>("UpgradeButton/Price");

        damageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/Damage/Value");
        critChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritChance/Value");
        critDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritDamage/Value");
        ballCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/BallCount/Value");

        upgradeDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/Damage/Value");
        upgradeCritChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/CritChance/Value");
        upgradeCritDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/CritDamage/Value");
        upgradeBallCountUI = transform.FindChild<TextMeshProUGUI>("Stats/UpgradeStats/BallCount/Value");

        infoDamage = transform.FindChild<Transform>("Stats/InfoPopups/Damage");
        infoCritChance = transform.FindChild<Transform>("Stats/InfoPopups/CritChance");
        infoCritDamage = transform.FindChild<Transform>("Stats/InfoPopups/CritDamage");
        infoBallCount = transform.FindChild<Transform>("Stats/InfoPopups/BallCount");

        infoDamage.gameObject.SetActive(false);
        infoCritChance.gameObject.SetActive(false);
        infoCritDamage.gameObject.SetActive(false);
        infoBallCount.gameObject.SetActive(false);

        ballController = FindObjectOfType<BallController>();
        gameStateController = FindObjectOfType<GameStateController>();

        BallStats.Instance.level = PersistentData.instance.ballData.level;

        BallStats.Instance.damage = PersistentData.instance.ballData.damage;
        BallStats.Instance.critChance = PersistentData.instance.ballData.critChance;
        BallStats.Instance.critDamage = PersistentData.instance.ballData.critDamage;
        BallStats.Instance.ballCount = PersistentData.instance.ballData.ballCount;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void Start() {
        ballController.SetMaxBallCount(BallStats.Instance.ballCount);
    }

    private void Update() {

        if (Input.GetMouseButton(0)) {
            if (InputHelper.instance.ClickedOnTag("InfoDamage")) {
                infoDamage.gameObject.SetActive(true);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoCritChance")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(true);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoCritDamage")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(true);
                infoBallCount.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoBallCount")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(true);
            }
        } else {
            infoDamage.gameObject.SetActive(false);
            infoCritChance.gameObject.SetActive(false);
            infoCritDamage.gameObject.SetActive(false);
            infoBallCount.gameObject.SetActive(false);
        }
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.level = BallStats.Instance.level;

        PersistentData.instance.ballData.damage = BallStats.Instance.damage;
        PersistentData.instance.ballData.critChance = BallStats.Instance.critChance;
        PersistentData.instance.ballData.critDamage = BallStats.Instance.critDamage;
        PersistentData.instance.ballData.ballCount = BallStats.Instance.ballCount;
    }

    private void UpdateUI() {
        scoreUI.text = "Skillpunkte: " + Score.instance.skillPoints.ToString();

        // Stats
        upgradePriceUI.text = BallStats.Instance.UpgradePrice.ToString();

        damageUI.text = BallStats.Instance.damage.ToString();
        critChanceUI.text = BallStats.Instance.critChance.ToString("0.00") + "%";
        critDamageUI.text = BallStats.Instance.critDamage.ToString("0.00") + "x";
        ballCountUI.text = BallStats.Instance.ballCount.ToString();

        upgradeDamageUI.text = "(+" + BallStats.Instance.UpgradeDamage.ToString() + ")";
        upgradeCritChanceUI.text = "(+" + BallStats.Instance.UpgradeCritChance.ToString("0.00") + "%)";
        upgradeCritDamageUI.text = "(+" + BallStats.Instance.UpgradeCritDamage.ToString("0.00") + "x)";
        upgradeBallCountUI.text = "(+" + BallStats.Instance.UpgradeBallCount.ToString() + ")";
    }
    
    public void UpgradeBallsOnClick() {
        if (Score.instance.PaySkillPoints(BallStats.Instance.UpgradePrice)) {
            BallStats.Instance.AddStats();
            ballController.SetMaxBallCount(BallStats.Instance.ballCount);
        }
        UpdateUI();
    }

    public void BallMenuButtonOnClick() {
        gameStateController.ballMenuButtonPressed = true;
        UpdateUI();
    }

    public void CloseButtonOnClick() {
        gameStateController.ballMenuCloseButtonPressed = true;
    }

    public void ResetData() {
        BallStats.Instance.ResetStats();
        UpdateUI();
    }

    public void Show() {
        GameController.instance.PauseGame();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
        gameStateController.optionsButtonPressed = false;
    }
}
