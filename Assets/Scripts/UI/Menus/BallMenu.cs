using UnityEngine;
using TMPro;

public class BallMenu : CanvasController {

    private TextMeshProUGUI damageUI;
    private TextMeshProUGUI critChanceUI;
    private TextMeshProUGUI critDamageUI;
    private TextMeshProUGUI ballCountUI;

    private Transform infoDamage;
    private Transform infoCritChance;
    private Transform infoCritDamage;
    private Transform infoBallCount;

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
        critChanceUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritChance/Value");
        critDamageUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/CritDamage/Value");
        ballCountUI = transform.FindChild<TextMeshProUGUI>("Stats/CurrentStats/BallCount/Value");

        infoDamage = transform.FindChild<Transform>("Stats/InfoPopups/Damage");
        infoCritChance = transform.FindChild<Transform>("Stats/InfoPopups/CritChance");
        infoCritDamage = transform.FindChild<Transform>("Stats/InfoPopups/CritDamage");
        infoBallCount = transform.FindChild<Transform>("Stats/InfoPopups/BallCount");

        infoDamage.gameObject.SetActive(false);
        infoCritChance.gameObject.SetActive(false);
        infoCritDamage.gameObject.SetActive(false);
        infoBallCount.gameObject.SetActive(false);

        ballController = FindObjectOfType<BallController>();

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

    public void PlayErrorSound() {
        errorSfx.Play();
    }

    public void PlaySuccessSound() {
        purchaseSfx.Play();
    }

    private void OnSaveGame() {
        PersistentData.instance.ballData.damage = BallStats.Instance.damage;
        PersistentData.instance.ballData.critChance = BallStats.Instance.critChance;
        PersistentData.instance.ballData.critDamage = BallStats.Instance.critDamage;
        PersistentData.instance.ballData.ballCount = BallStats.Instance.ballCount;
    }

    public void UpdateUI() {

        damageUI.text = BallStats.Instance.damage.ToString();
        critChanceUI.text = BallStats.Instance.critChance.ToString("0") + "%";
        critDamageUI.text = BallStats.Instance.critDamage.ToString("0") + "x";
        ballCountUI.text = BallStats.Instance.ballCount.ToString();
    }

    public void ResetData() {
        BallStats.Instance.ResetStats();
        UpdateUI();
    }

    public override void Show() {
        benitrator.SetInitialBetIfEnoughSkillPoints();
        UpdateUI();
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f);
    }
}
