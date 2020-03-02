using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallMenu : CanvasController {

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
    private Image upgradeButtonBackground;

    private MoveUI moveUI;
    private BallController ballController;

    private AudioSource errorSfx;
    private AudioSource purchaseSfx;

    private void Awake() {
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();

        moveUI = GetComponent<MoveUI>();

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
        upgradeButtonBackground = transform.FindChild<Image>("UpgradeButton/Background");

        infoDamage.gameObject.SetActive(false);
        infoCritChance.gameObject.SetActive(false);
        infoCritDamage.gameObject.SetActive(false);
        infoBallCount.gameObject.SetActive(false);


        ballController = FindObjectOfType<BallController>();

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

        if (Score.instance.skillPoints < BallStats.Instance.UpgradePrice)
            upgradeButtonBackground.color = new Color(0.35f, 0.35f, 0.35f, 1f);
        else
            upgradeButtonBackground.color = new Color(0.8235295f, 0.2352941f, 0.1333333f, 1f);
    }

    public void UpgradeBallsOnClick() {
        if (Score.instance.PaySkillPoints(BallStats.Instance.UpgradePrice)) {
            BallStats.Instance.AddStats();
            ballController.SetMaxBallCount(BallStats.Instance.ballCount);
            purchaseSfx.Play();
        } else {
            errorSfx.Play();
        }
        UpdateUI();
    }

    public void ResetData() {
        BallStats.Instance.ResetStats();
        UpdateUI();
    }

    public override void Show() {
        UpdateUI();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
    }
}
