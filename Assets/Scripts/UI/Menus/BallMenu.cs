using UnityEngine;
using TMPro;

public class BallMenu : CanvasController {

    public FloatingText floatingTextPrefab;

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI ballCountUI;
    private TextMeshProUGUI critChanceUI;
    private TextMeshProUGUI critDamageUI;

    private Transform infoDamage;
    private Transform infoBallCount;
    private Transform infoCritChance;
    private Transform infoCritDamage;

    private Transform infoDangerLevel;
    private Transform dangerLevelTransform;

    private MoveUI moveUI;
    private BallController ballController;

    private AudioSource errorSfx;
    private AudioSource purchaseSfx;

    private Benitrator benitrator;

    private void Awake() {
        errorSfx = GameObject.Find("SfxError").GetComponent<AudioSource>();
        purchaseSfx = GameObject.Find("SfxUnlockSkill").GetComponent<AudioSource>();

        moveUI = GetComponent<MoveUI>();
        benitrator = FindObjectOfType<Benitrator>();

        damageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/Damage/Value");
        ballCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/BallCount/Value");
        critChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritChance/Value");
        critDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritDamage/Value");

        infoDamage = transform.FindChild<Transform>("Stats/InfoPopups/Damage");
        infoBallCount = transform.FindChild<Transform>("Stats/InfoPopups/BallCount");
        infoCritChance = transform.FindChild<Transform>("Stats/InfoPopups/CritChance");
        infoCritDamage = transform.FindChild<Transform>("Stats/InfoPopups/CritDamage");

        dangerLevelTransform = transform.FindChild<Transform>("Benitrator/DangerLevel");
        infoDangerLevel = transform.FindChild<Transform>("Benitrator/DangerLevel/InfoPopup");

        infoDamage.gameObject.SetActive(false);
        infoBallCount.gameObject.SetActive(false);
        infoCritChance.gameObject.SetActive(false);
        infoCritDamage.gameObject.SetActive(false);
        infoDangerLevel.gameObject.SetActive(false);

        ballController = FindObjectOfType<BallController>();

        BallStats.Instance.damage = PersistentData.instance.ballData.damage;
        BallStats.Instance.ballCount = PersistentData.instance.ballData.ballCount;
        BallStats.Instance.critChance = PersistentData.instance.ballData.critChance;
        BallStats.Instance.critDamage = PersistentData.instance.ballData.critDamage;

        EventManager.StartListening("ChacheData", OnChacheData);
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
                infoDangerLevel.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoCritChance")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(true);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(false);
                infoDangerLevel.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoCritDamage")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(true);
                infoBallCount.gameObject.SetActive(false);
                infoDangerLevel.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoBallCount")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(true);
                infoDangerLevel.gameObject.SetActive(false);
            }

            if (InputHelper.instance.ClickedOnTag("InfoDangerLevel")) {
                infoDamage.gameObject.SetActive(false);
                infoCritChance.gameObject.SetActive(false);
                infoCritDamage.gameObject.SetActive(false);
                infoBallCount.gameObject.SetActive(false);
                infoDangerLevel.gameObject.SetActive(true);
            }
        } else {
            infoDamage.gameObject.SetActive(false);
            infoCritChance.gameObject.SetActive(false);
            infoCritDamage.gameObject.SetActive(false);
            infoBallCount.gameObject.SetActive(false);
            infoDangerLevel.gameObject.SetActive(false);
        }
    }

    public void PlayErrorSound() {
        errorSfx.Play();
    }

    public void PlaySuccessSound() {
        purchaseSfx.Play();
    }

    private void OnChacheData() {
        PersistentData.instance.ballData.damage = BallStats.Instance.damage;
        PersistentData.instance.ballData.critChance = BallStats.Instance.critChance;
        PersistentData.instance.ballData.critDamage = BallStats.Instance.critDamage;
        PersistentData.instance.ballData.ballCount = BallStats.Instance.ballCount;
    }

    public void UpdateUI() {
        damageUI.text = BallStats.Instance.damage.ToString();
        critChanceUI.text = BallStats.Instance.critChance.ToString("0") + "%";
        critDamageUI.text = BallStats.Instance.critDamage.ToString("0.##") + "x";
        ballCountUI.text = BallStats.Instance.ballCount.ToString();
    }

    public void ResetData() {
        BallStats.Instance.ResetStats();
        UpdateUI();
    }

    public void ShowFloatingTextDangerLevel(int value) {
        FloatingText floatingText = Instantiate(floatingTextPrefab, dangerLevelTransform.position, Quaternion.identity);
        floatingText.SetText(value.ToString());
    }

    public override void Show() {
        benitrator.ShowProChanIfZeroSkillPoints();
        benitrator.SetInitialBetIfEnoughSkillPoints();
        UpdateUI();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
    }
}
